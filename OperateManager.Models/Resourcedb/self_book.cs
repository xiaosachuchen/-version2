using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 图书资源管理 self_book 
	 /// </summary>
	 public partial class self_book : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_book 
		 /// </summary>
		 public self_book() 
		 { }

		 /// <summary>
		 /// 构造函数 self_book 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_booknum">书号</param> 
		 /// <param name="_bookname">书名</param> 
		 /// <param name="_bookenname">英文书名</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_bookclass">图书分类</param> 
		 /// <param name="_subname">副书名</param> 
		 /// <param name="_branchname">分辑名</param> 
		 /// <param name="_branchno">分辑号</param> 
		 /// <param name="_seriesname">丛书名</param> 
		 /// <param name="_seriesno">丛书卷号</param> 
		 /// <param name="_authorstr">作者</param> 
		 /// <param name="_translator">译者</param> 
		 /// <param name="_pubplace">出版地</param> 
		 /// <param name="_pressid">出版社</param> 
		 /// <param name="_pubdate">出版日期</param> 
		 /// <param name="_pubyear">出版年</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_isbn">isbn</param> 
		 /// <param name="_barcode">条形码</param> 
		 /// <param name="_version">版本</param> 
		 /// <param name="_binding">装帧</param> 
		 /// <param name="_pagecount">总页数</param> 
		 /// <param name="_subjectwords">主题词</param> 
		 /// <param name="_clc">中图分类号</param> 
		 /// <param name="_asin">亚马逊产品编号</param> 
		 /// <param name="_brand">品牌</param> 
		 /// <param name="_charcount">字数(千字)</param> 
		 /// <param name="_size">开本</param> 
		 /// <param name="_impression">印次</param> 
		 /// <param name="_m_price">定价</param> 
		 /// <param name="_m_discount">折扣</param> 
		 /// <param name="_language">语种</param> 
		 /// <param name="_abstracts">摘要</param> 
		 /// <param name="_seriesabstracts">丛书简介</param> 
		 /// <param name="_reference">参考文献</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_paraxml">目录</param> 
		 /// <param name="_quotes">引用图书</param> 
		 /// <param name="_coverpath">封面</param> 
		 /// <param name="_editorrec">编辑推荐</param> 
		 /// <param name="_celebrityrec">名人推荐</param> 
		 /// <param name="_preface">序言</param> 
		 /// <param name="_digest">文摘</param> 
		 /// <param name="_copyrightno">版权页</param> 
		 /// <param name="_fullpath">全文路径</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_book(int? _seqid,string _booknum,string _bookname,string _bookenname,string _subjectclass,string _bookclass,string _subname,string _branchname,string _branchno,string _seriesname,string _seriesno,int? _authorstr,string _translator,string _pubplace,int? _pressid,DateTime? _pubdate,DateTime? _pubyear,int? _orgid,string _isbn,string _barcode,string _version,string _binding,int? _pagecount,string _subjectwords,string _clc,string _asin,string _brand,decimal? _charcount,string _size,string _impression,decimal? _m_price,decimal? _m_discount,int? _language,string _abstracts,string _seriesabstracts,string _reference,int? _isfull,string _paraxml,string _quotes,string _coverpath,string _editorrec,string _celebrityrec,string _preface,string _digest,string _copyrightno,string _fullpath,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.booknum = _booknum;
			 this.bookname = _bookname;
			 this.bookenname = _bookenname;
			 this.subjectclass = _subjectclass;
			 this.bookclass = _bookclass;
			 this.subname = _subname;
			 this.branchname = _branchname;
			 this.branchno = _branchno;
			 this.seriesname = _seriesname;
			 this.seriesno = _seriesno;
			 this.authorstr = _authorstr;
			 this.translator = _translator;
			 this.pubplace = _pubplace;
			 this.pressid = _pressid;
			 this.pubdate = _pubdate;
			 this.pubyear = _pubyear;
			 this.orgid = _orgid;
			 this.isbn = _isbn;
			 this.barcode = _barcode;
			 this.version = _version;
			 this.binding = _binding;
			 this.pagecount = _pagecount;
			 this.subjectwords = _subjectwords;
			 this.clc = _clc;
			 this.asin = _asin;
			 this.brand = _brand;
			 this.charcount = _charcount;
			 this.size = _size;
			 this.impression = _impression;
			 this.m_price = _m_price;
			 this.m_discount = _m_discount;
			 this.language = _language;
			 this.abstracts = _abstracts;
			 this.seriesabstracts = _seriesabstracts;
			 this.reference = _reference;
			 this.isfull = _isfull;
			 this.paraxml = _paraxml;
			 this.quotes = _quotes;
			 this.coverpath = _coverpath;
			 this.editorrec = _editorrec;
			 this.celebrityrec = _celebrityrec;
			 this.preface = _preface;
			 this.digest = _digest;
			 this.copyrightno = _copyrightno;
			 this.fullpath = _fullpath;
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
		 /// 书号 
		 /// <summary>
		 [Display(Name = "书号")] 
		 public string booknum{ get; set; } 

		 /// <summary>
		 /// 书名 
		 /// <summary>
		 [Display(Name = "书名")] 
		 public string bookname{ get; set; } 

		 /// <summary>
		 /// 英文书名 
		 /// <summary>
		 [Display(Name = "英文书名")] 
		 public string bookenname{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 图书分类 
		 /// <summary>
		 [Display(Name = "图书分类")] 
		 public string bookclass{ get; set; } 

		 /// <summary>
		 /// 副书名 
		 /// <summary>
		 [Display(Name = "副书名")] 
		 public string subname{ get; set; } 

		 /// <summary>
		 /// 分辑名 
		 /// <summary>
		 [Display(Name = "分辑名")] 
		 public string branchname{ get; set; } 

		 /// <summary>
		 /// 分辑号 
		 /// <summary>
		 [Display(Name = "分辑号")] 
		 public string branchno{ get; set; } 

		 /// <summary>
		 /// 丛书名 
		 /// <summary>
		 [Display(Name = "丛书名")] 
		 public string seriesname{ get; set; } 

		 /// <summary>
		 /// 丛书卷号 
		 /// <summary>
		 [Display(Name = "丛书卷号")] 
		 public string seriesno{ get; set; } 

		 /// <summary>
		 /// 作者 
		 /// <summary>
		 [Display(Name = "作者")] 
		 public int? authorstr{ get; set; } 

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
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// isbn 
		 /// <summary>
		 [Display(Name = "isbn")] 
		 public string isbn{ get; set; } 

		 /// <summary>
		 /// 条形码 
		 /// <summary>
		 [Display(Name = "条形码")] 
		 public string barcode{ get; set; } 

		 /// <summary>
		 /// 版本 
		 /// <summary>
		 [Display(Name = "版本")] 
		 public string version{ get; set; } 

		 /// <summary>
		 /// 装帧 
		 /// <summary>
		 [Display(Name = "装帧")] 
		 public string binding{ get; set; } 

		 /// <summary>
		 /// 总页数 
		 /// <summary>
		 [Display(Name = "总页数")] 
		 public int? pagecount{ get; set; } 

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
		 /// 亚马逊产品编号 
		 /// <summary>
		 [Display(Name = "亚马逊产品编号")] 
		 public string asin{ get; set; } 

		 /// <summary>
		 /// 品牌 
		 /// <summary>
		 [Display(Name = "品牌")] 
		 public string brand{ get; set; } 

		 /// <summary>
		 /// 字数(千字) 
		 /// <summary>
		 [Display(Name = "字数(千字)")] 
		 public decimal? charcount{ get; set; } 

		 /// <summary>
		 /// 开本 
		 /// <summary>
		 [Display(Name = "开本")] 
		 public string size{ get; set; } 

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
		 /// 摘要 
		 /// <summary>
		 [Display(Name = "摘要")] 
		 public string abstracts{ get; set; } 

		 /// <summary>
		 /// 丛书简介 
		 /// <summary>
		 [Display(Name = "丛书简介")] 
		 public string seriesabstracts{ get; set; } 

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
		 /// 目录 
		 /// <summary>
		 [Display(Name = "目录")] 
		 public string paraxml{ get; set; } 

		 /// <summary>
		 /// 引用图书 
		 /// <summary>
		 [Display(Name = "引用图书")] 
		 public string quotes{ get; set; } 

		 /// <summary>
		 /// 封面 
		 /// <summary>
		 [Display(Name = "封面")] 
		 public string coverpath{ get; set; } 

		 /// <summary>
		 /// 编辑推荐 
		 /// <summary>
		 [Display(Name = "编辑推荐")] 
		 public string editorrec{ get; set; } 

		 /// <summary>
		 /// 名人推荐 
		 /// <summary>
		 [Display(Name = "名人推荐")] 
		 public string celebrityrec{ get; set; } 

		 /// <summary>
		 /// 序言 
		 /// <summary>
		 [Display(Name = "序言")] 
		 public string preface{ get; set; } 

		 /// <summary>
		 /// 文摘 
		 /// <summary>
		 [Display(Name = "文摘")] 
		 public string digest{ get; set; } 

		 /// <summary>
		 /// 版权页 
		 /// <summary>
		 [Display(Name = "版权页")] 
		 public string copyrightno{ get; set; } 

		 /// <summary>
		 /// 全文路径 
		 /// <summary>
		 [Display(Name = "全文路径")] 
		 public string fullpath{ get; set; } 

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
			 self_book model = obj as self_book;
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
