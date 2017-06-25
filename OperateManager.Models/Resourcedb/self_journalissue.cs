using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 刊期管理 self_journalissue 
	 /// </summary>
	 public partial class self_journalissue : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_journalissue 
		 /// </summary>
		 public self_journalissue() 
		 { }

		 /// <summary>
		 /// 构造函数 self_journalissue 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_journalid">期刊编号</param> 
		 /// <param name="_issuetitle">卷期名称</param> 
		 /// <param name="_year">年</param> 
		 /// <param name="_issue">期</param> 
		 /// <param name="_pubdate">出版日期</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_journalissue(int? _seqid,int? _journalid,string _issuetitle,DateTime? _year,string _issue,DateTime? _pubdate,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.journalid = _journalid;
			 this.issuetitle = _issuetitle;
			 this.year = _year;
			 this.issue = _issue;
			 this.pubdate = _pubdate;
			 this.hits = _hits;
			 this.createdtime = _createdtime;
			 this.creatorid = _creatorid;
			 this.status = _status;
		 }

		 #region 实体属性
		 /// <summary>
		 /// 编号 
		 /// <summary>
		 [Display(Name = "编号")] 
		 public int? seqid{ get; set; } 

		 /// <summary>
		 /// 期刊编号 
		 /// <summary>
		 [Display(Name = "期刊编号")] 
		 public int? journalid{ get; set; } 

		 /// <summary>
		 /// 卷期名称 
		 /// <summary>
		 [Display(Name = "卷期名称")] 
		 public string issuetitle{ get; set; } 

		 /// <summary>
		 /// 年 
		 /// <summary>
		 [Display(Name = "年")] 
		 public DateTime? year{ get; set; } 

		 /// <summary>
		 /// 期 
		 /// <summary>
		 [Display(Name = "期")] 
		 public string issue{ get; set; } 

		 /// <summary>
		 /// 出版日期 
		 /// <summary>
		 [Display(Name = "出版日期")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 点击量 
		 /// <summary>
		 [Display(Name = "点击量")] 
		 public int? hits{ get; set; } 

		 /// <summary>
		 /// 创建时间 
		 /// <summary>
		 [Display(Name = "创建时间")] 
		 public DateTime? createdtime{ get; set; } 

		 /// <summary>
		 /// 创建人 
		 /// <summary>
		 [Display(Name = "创建人")] 
		 public int? creatorid{ get; set; } 

		 /// <summary>
		 /// 资源状态 
		 /// <summary>
		 [Display(Name = "资源状态")] 
		 public int? status{ get; set; } 

		 #endregion

		 #region ICloneable 成员
		 public object Clone()
		 {
			 return this.MemberwiseClone();
		 }
		 #endregion

		 public override bool Equals(object obj)
		 {
			 self_journalissue model = obj as self_journalissue;
			 if (model != null && model.seqid == this.seqid) 
				 return true;
			 return false;
		 }

		 public override int GetHashCode()
		 {
			 return seqid.GetHashCode();
		 }
	 }
}
