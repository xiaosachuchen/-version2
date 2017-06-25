using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 报告管理 self_report 
	 /// </summary>
	 public partial class self_report : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_report 
		 /// </summary>
		 public self_report() 
		 { }

		 /// <summary>
		 /// 构造函数 self_report 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_title">报告名</param> 
		 /// <param name="_type">报告类型</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_branchname">分辑名</param> 
		 /// <param name="_seriesname">丛书名</param> 
		 /// <param name="_coverpath">封面</param> 
		 /// <param name="_author">作者</param> 
		 /// <param name="_translator">译者</param> 
		 /// <param name="_pubplace">出版地</param> 
		 /// <param name="_pressid">出版社</param> 
		 /// <param name="_pubdate">出版日期</param> 
		 /// <param name="_pubyear">出版年</param> 
		 /// <param name="_subjectwords">主题词</param> 
		 /// <param name="_clc">中图分类号</param> 
		 /// <param name="_booknum">书号</param> 
		 /// <param name="_isbn">isbn</param> 
		 /// <param name="_size">开本</param> 
		 /// <param name="_charcount">字数(千字)</param> 
		 /// <param name="_binding">装帧</param> 
		 /// <param name="_impression">印次</param> 
		 /// <param name="_m_price">定价</param> 
		 /// <param name="_m_discount">折扣</param> 
		 /// <param name="_language">语种</param> 
		 /// <param name="_authorintro">作者简介</param> 
		 /// <param name="_abstracts">摘要</param> 
		 /// <param name="_paraxml">目录</param> 
		 /// <param name="_highlight">精彩片段</param> 
		 /// <param name="_review">书评</param> 
		 /// <param name="_foundation">基金</param> 
		 /// <param name="_reference">参考文献</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_report(int? _seqid,string _title,int? _type,string _subjectclass,int? _orgid,string _branchname,string _seriesname,string _coverpath,string _author,string _translator,string _pubplace,int? _pressid,DateTime? _pubdate,DateTime? _pubyear,string _subjectwords,string _clc,string _booknum,string _isbn,string _size,decimal? _charcount,string _binding,string _impression,decimal? _m_price,decimal? _m_discount,int? _language,string _authorintro,string _abstracts,string _paraxml,string _highlight,string _review,string _foundation,string _reference,int? _isfull,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.title = _title;
			 this.type = _type;
			 this.subjectclass = _subjectclass;
			 this.orgid = _orgid;
			 this.branchname = _branchname;
			 this.seriesname = _seriesname;
			 this.coverpath = _coverpath;
			 this.author = _author;
			 this.translator = _translator;
			 this.pubplace = _pubplace;
			 this.pressid = _pressid;
			 this.pubdate = _pubdate;
			 this.pubyear = _pubyear;
			 this.subjectwords = _subjectwords;
			 this.clc = _clc;
			 this.booknum = _booknum;
			 this.isbn = _isbn;
			 this.size = _size;
			 this.charcount = _charcount;
			 this.binding = _binding;
			 this.impression = _impression;
			 this.m_price = _m_price;
			 this.m_discount = _m_discount;
			 this.language = _language;
			 this.authorintro = _authorintro;
			 this.abstracts = _abstracts;
			 this.paraxml = _paraxml;
			 this.highlight = _highlight;
			 this.review = _review;
			 this.foundation = _foundation;
			 this.reference = _reference;
			 this.isfull = _isfull;
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
		 /// 报告名 
		 /// <summary>
		 [Display(Name = "报告名")] 
		 public string title{ get; set; } 

		 /// <summary>
		 /// 报告类型 
		 /// <summary>
		 [Display(Name = "报告类型")] 
		 public int? type{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// 分辑名 
		 /// <summary>
		 [Display(Name = "分辑名")] 
		 public string branchname{ get; set; } 

		 /// <summary>
		 /// 丛书名 
		 /// <summary>
		 [Display(Name = "丛书名")] 
		 public string seriesname{ get; set; } 

		 /// <summary>
		 /// 封面 
		 /// <summary>
		 [Display(Name = "封面")] 
		 public string coverpath{ get; set; } 

		 /// <summary>
		 /// 作者 
		 /// <summary>
		 [Display(Name = "作者")] 
		 public string author{ get; set; } 

		 /// <summary>
		 /// 译者 
		 /// <summary>
		 [Display(Name = "译者")] 
		 public string translator{ get; set; } 

		 /// <summary>
		 /// 出版地 
		 /// <summary>
		 [Display(Name = "出版地")] 
		 public string pubplace{ get; set; } 

		 /// <summary>
		 /// 出版社 
		 /// <summary>
		 [Display(Name = "出版社")] 
		 public int? pressid{ get; set; } 

		 /// <summary>
		 /// 出版日期 
		 /// <summary>
		 [Display(Name = "出版日期")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 出版年 
		 /// <summary>
		 [Display(Name = "出版年")] 
		 public DateTime? pubyear{ get; set; } 

		 /// <summary>
		 /// 主题词 
		 /// <summary>
		 [Display(Name = "主题词")] 
		 public string subjectwords{ get; set; } 

		 /// <summary>
		 /// 中图分类号 
		 /// <summary>
		 [Display(Name = "中图分类号")] 
		 public string clc{ get; set; } 

		 /// <summary>
		 /// 书号 
		 /// <summary>
		 [Display(Name = "书号")] 
		 public string booknum{ get; set; } 

		 /// <summary>
		 /// isbn 
		 /// <summary>
		 [Display(Name = "isbn")] 
		 public string isbn{ get; set; } 

		 /// <summary>
		 /// 开本 
		 /// <summary>
		 [Display(Name = "开本")] 
		 public string size{ get; set; } 

		 /// <summary>
		 /// 字数(千字) 
		 /// <summary>
		 [Display(Name = "字数(千字)")] 
		 public decimal? charcount{ get; set; } 

		 /// <summary>
		 /// 装帧 
		 /// <summary>
		 [Display(Name = "装帧")] 
		 public string binding{ get; set; } 

		 /// <summary>
		 /// 印次 
		 /// <summary>
		 [Display(Name = "印次")] 
		 public string impression{ get; set; } 

		 /// <summary>
		 /// 定价 
		 /// <summary>
		 [Display(Name = "定价")] 
		 public decimal? m_price{ get; set; } 

		 /// <summary>
		 /// 折扣 
		 /// <summary>
		 [Display(Name = "折扣")] 
		 public decimal? m_discount{ get; set; } 

		 /// <summary>
		 /// 语种 
		 /// <summary>
		 [Display(Name = "语种")] 
		 public int? language{ get; set; } 

		 /// <summary>
		 /// 作者简介 
		 /// <summary>
		 [Display(Name = "作者简介")] 
		 public string authorintro{ get; set; } 

		 /// <summary>
		 /// 摘要 
		 /// <summary>
		 [Display(Name = "摘要")] 
		 public string abstracts{ get; set; } 

		 /// <summary>
		 /// 目录 
		 /// <summary>
		 [Display(Name = "目录")] 
		 public string paraxml{ get; set; } 

		 /// <summary>
		 /// 精彩片段 
		 /// <summary>
		 [Display(Name = "精彩片段")] 
		 public string highlight{ get; set; } 

		 /// <summary>
		 /// 书评 
		 /// <summary>
		 [Display(Name = "书评")] 
		 public string review{ get; set; } 

		 /// <summary>
		 /// 基金 
		 /// <summary>
		 [Display(Name = "基金")] 
		 public string foundation{ get; set; } 

		 /// <summary>
		 /// 参考文献 
		 /// <summary>
		 [Display(Name = "参考文献")] 
		 public string reference{ get; set; } 

		 /// <summary>
		 /// 是否全文0否1是 
		 /// <summary>
		 [Display(Name = "是否全文0否1是")] 
		 public int? isfull{ get; set; } 

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
			 self_report model = obj as self_report;
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
