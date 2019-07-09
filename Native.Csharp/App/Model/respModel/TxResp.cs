﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Model.respModel
{

    public class TxResp<T> where T:class
    {
        public int code { get; set; }
        public string msg { get; set; }
        public List<T> newslist { get; set; }
    }
    public class TrashSortResp
    {
        /// <summary>
        /// 废弃物名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 垃圾分类，0为可回收、1为有害、2为厨余(湿)、3为其他(干)
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 分类解释
        /// </summary>
        public string explain { get; set; }
        /// <summary>
        /// 包含类型
        /// </summary>
        public string contain { get; set; }
        /// <summary>
        /// 投放提示
        /// </summary>
        public string tip { get; set; }
    }
}
