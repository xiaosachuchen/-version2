using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 书评管理 self_reviewarticle 
	 /// </summary>
	 public partial class self_reviewarticle : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_reviewarticle 
		 /// </summary>
		 public self_reviewarticle() 
		 { }

		 /// <summary>
		 /// 构造函数 self_reviewarticle 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_title">标题</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_type">书评类型1书评2附录</param> 
		 /// <param name="_reviewerid">书评人编号</param> 
		 /// <param name="_book">所评图书</param> 
		 /// <param name="_pubdate">发表时间</param> 
		 /// <param name="_journalid">发表刊物</param> 
		 /// <param name="_journal">发表刊物名称</param> 
		 /// <param name="_issue">刊期</param> 
		 /// <param name="_introduction">简介</param> 
		 /// <param name="_text">正文</param> 
		 /// <param name="_website">参考网址</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_reviewarticle(int? _seqid,string _title,int? _subjectclass,int? _type,int? _reviewerid,string _book,DateTime? _pubdate,int? _journalid,string _journal,string _issue,string _introduction,string _text,string _website,int? _orgid,int? _hits,int? _isfull,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.title = _title;
			 this.subjectclass = _subjectclass;
			 this.type = _type;
			 this.reviewerid = _reviewerid;
			 this.book = _book;
			 this.pubdate = _pubdate;
			 this.journalid = _journalid;
			 this.journal = _journal;
			 this.issue = _issue;
			 this.introduction = _introduction;
			 this.text = _text;
			 this.website = _website;
			 this.orgid = _orgid;
			 this.hits = _hits;
			 this.isfull = _isfull;
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
		 /// 标题 
		 /// <summary>
		 [Display(Name = "标题")] 
		 public string title{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public int? subjectclass{ get; set; } 

		 /// <summary>
		 /// 书评类型1书评2附录 
		 /// <summary>
		 [Display(Name = "书评类型1书评2附录")] 
		 public int? type{ get; set; } 

		 /// <summary>
		 /// 书评人编号 
		 /// <summary>
		 [Display(Name = "书评人编号")] 
		 public int? reviewerid{ get; set; } 

		 /// <summary>
		 /// 所评图书 
		 /// <summary>
		 [Display(Name = "所评图书")] 
		 public string book{ get; set; } 

		 /// <summary>
		 /// 发表时间 
		 /// <summary>
		 [Display(Name = "发表时间")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 发表刊物 
		 /// <summary>
		 [Display(Name = "发表刊物")] 
		 public int? journalid{ get; set; } 

		 /// <summary>
		 /// 发表刊物名称 
		 /// <summary>
		 [Display(Name = "发表刊物名称")] 
		 public string journal{ get; set; } 

		 /// <summary>
		 /// 刊期 
		 /// <summary>
		 [Display(Name = "刊期")] 
		 public string issue{ get; set; } 

		 /// <summary>
		 /// 简介 
		 /// <summary>
		 [Display(Name = "简介")] 
		 public string introduction{ get; set; } 

		 /// <summary>
		 /// 正文 
		 /// <summary>
		 [Display(Name = "正文")] 
		 public string text{ get; set; } 

		 /// <summary>
		 /// 参考网址 
		 /// <summary>
		 [Display(Name = "参考网址")] 
		 public string website{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// 点击量 
		 /// <summary>
		 [Display(Name = "点击量")] 
		 public int? hits{ get; set; } 

		 /// <summary>
		 /// 是否全文0否1是 
		 /// <summary>
		 [Display(Name = "是否全文0否1是")] 
		 public int? isfull{ get; set; } 

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
			 self_reviewarticle model = obj as self_reviewarticle;
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
