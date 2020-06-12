using Site.Traceless.Common.Model;
using Site.Traceless.Plugin.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Site.Traceless.Nativedemo.Code.Event
{
    public class PluginStore
    {
        public static Dictionary<int, IBasePlugin> plugins = new Dictionary<int, IBasePlugin>();

        public static int InitPlugIn()
        {
            CommonData.CqLog.Info("初始化", "插件扫描开始");
            string dicPath = CommonData.CqApi.AppDirectory + "plugin\\";
            string[] files = Directory.GetFiles(dicPath);
            if (files == null)
            {
                CommonData.CqLog.Info("插件扫描", "没有插件");
                return 0;
            }
            foreach (var f in files)
            {
                if (!f.ToUpper().EndsWith(".DLL"))
                    continue;
                try
                {
                    //先把DLL加载到内存，再从内存中加载（可在程序运行时动态更新dll文件，比借助AppDomain方便多了！）
                    using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bFile = br.ReadBytes((int)fs.Length);
                            br.Close();
                            fs.Close();
                            Assembly ab = Assembly.Load(bFile);
                            Type[] t = ab.GetTypes();
                            foreach (var x in t)
                            {
                                if (x.GetInterface("IBasePlugin") != null)
                                {
                                    IBasePlugin nowPlugin = (IBasePlugin)ab.CreateInstance(x.FullName);
                                    plugins.Add(nowPlugin.GetHashCode(), nowPlugin);
                                    CommonData.CqLog.Info("插件扫描", $"寻找到插件{nowPlugin.Name}");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonData.CqLog.Fatal("插件扫描", ex.ToString());
                }
            }
            CommonData.CqLog.Info("初始化", $"插件扫描完成，总计{plugins.Count()}个");
            return plugins.Count();
        }

        public static void Add(IBasePlugin plugin)
        {
            if (plugins.ContainsKey(plugin.GetHashCode()))
            {
                //已存在，不放入
            }
            else
            {
                plugins.Add(plugin.GetHashCode(), plugin);
            }
        }

        public static IBasePlugin GetPlugin(int hashCode)
        {
            if (plugins.ContainsKey(hashCode))
            {
                plugins.TryGetValue(hashCode, out IBasePlugin ret);
                return ret;
            }
            else
            {
                return null;
            }
        }

        public static IBasePlugin GetPluginPcmd(string pcmd)
        {
            return plugins.Values.Where(p => p.PCommand == pcmd).FirstOrDefault();
        }

        public static IBasePlugin GetPluginGcmd(string gcmd)
        {
            return plugins.Values.Where(p => p.GCommand == gcmd).FirstOrDefault();
        }
    }
}