using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 期刊论文管理 self_journalarticle 
	 /// </summary>
	 public partial class self_journalarticle : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_journalarticle 
		 /// </summary>
		 public self_journalarticle() 
		 { }

		 /// <summary>
		 /// 构造函数 self_journalarticle 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_journalid">期刊编号</param> 
		 /// <param name="_issue">刊期id</param> 
		 /// <param name="_title">中文标题</param> 
		 /// <param name="_entitle">英文标题</param> 
		 /// <param name="_author">中文名</param> 
		 /// <param name="_enauthor">英文名</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_journal">期刊名称</param> 
		 /// <param name="_enjournal">期刊英文名</param> 
		 /// <param name="_keyword">关键词</param> 
		 /// <param name="_enkeyword">英文关键词</param> 
		 /// <param name="_abstracts">中文摘要</param> 
		 /// <param name="_enabstracts">英文摘要</param> 
		 /// <param name="_foundation">基金</param> 
		 /// <param name="_pubdate">发表时间</param> 
		 /// <param name="_level">研究层次</param> 
		 /// <param name="_reference">参考文献</param> 
		 /// <param name="_quotes">引用文献</param> 
		 /// <param name="_pubyear">出版年</param> 
		 /// <param name="_issn">国际标准刊号</param> 
		 /// <param name="_cn">国内统一刊号</param> 
		 /// <param name="_pagenum">页码</param> 
		 /// <param name="_course">学科分类</param> 
		 /// <param name="_clc">中图分类号</param> 
		 /// <param name="_sci">sci收录刊</param> 
		 /// <param name="_ei">ei收录刊</param> 
		 /// <param name="_corejournal">核心期刊</param> 
		 /// <param name="_theme">主题</param> 
		 /// <param name="_language">语言</param> 
		 /// <param name="_pubunit">出版单位</param> 
		 /// <param name="_textpicture">正文快照</param> 
		 /// <param name="_fullpath">全文路径</param> 
		 /// <param name="_commauthor">通讯作者</param> 
		 /// <param name="_doi">数字对象唯一标识符</param> 
		 /// <param name="_quotenum">被引用量</param> 
		 /// <param name="_similarissue">相似文献</param> 
		 /// <param name="_relateblog">相关博文</param> 
		 /// <param name="_relatescholars">相关学者</param> 
		 /// <param name="_relateword">相关检索词</param> 
		 /// <param name="_mlcn">机标分类号</param> 
		 /// <param name="_relateissue">相关文献</param> 
		 /// <param name="_authorissues">作者其他文献</param> 
		 /// <param name="_institutionissue">机构其他文献</param> 
		 /// <param name="_peerissue">同行关注文献</param> 
		 /// <param name="_picture">图片</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_journalarticle(int? _seqid,int? _journalid,int? _issue,string _title,string _entitle,string _author,string _enauthor,int? _orgid,string _journal,string _enjournal,string _keyword,string _enkeyword,string _abstracts,string _enabstracts,string _foundation,DateTime? _pubdate,string _level,string _reference,string _quotes,DateTime? _pubyear,string _issn,string _cn,int? _pagenum,string _course,string _clc,string _sci,string _ei,string _corejournal,string _theme,int? _language,string _pubunit,string _textpicture,string _fullpath,string _commauthor,string _doi,int? _quotenum,string _similarissue,string _relateblog,string _relatescholars,string _relateword,string _mlcn,string _relateissue,string _authorissues,string _institutionissue,string _peerissue,string _picture,int? _isfull,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.journalid = _journalid;
			 this.issue = _issue;
			 this.title = _title;
			 this.entitle = _entitle;
			 this.author = _author;
			 this.enauthor = _enauthor;
			 this.orgid = _orgid;
			 this.journal = _journal;
			 this.enjournal = _enjournal;
			 this.keyword = _keyword;
			 this.enkeyword = _enkeyword;
			 this.abstracts = _abstracts;
			 this.enabstracts = _enabstracts;
			 this.foundation = _foundation;
			 this.pubdate = _pubdate;
			 this.level = _level;
			 this.reference = _reference;
			 this.quotes = _quotes;
			 this.pubyear = _pubyear;
			 this.issn = _issn;
			 this.cn = _cn;
			 this.pagenum = _pagenum;
			 this.course = _course;
			 this.clc = _clc;
			 this.sci = _sci;
			 this.ei = _ei;
			 this.corejournal = _corejournal;
			 this.theme = _theme;
			 this.language = _language;
			 this.pubunit = _pubunit;
			 this.textpicture = _textpicture;
			 this.fullpath = _fullpath;
			 this.commauthor = _commauthor;
			 this.doi = _doi;
			 this.quotenum = _quotenum;
			 this.similarissue = _similarissue;
			 this.relateblog = _relateblog;
			 this.relatescholars = _relatescholars;
			 this.relateword = _relateword;
			 this.mlcn = _mlcn;
			 this.relateissue = _relateissue;
			 this.authorissues = _authorissues;
			 this.institutionissue = _institutionissue;
			 this.peerissue = _peerissue;
			 this.picture = _picture;
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
		 /// 期刊编号 
		 /// <summary>
		 [Display(Name = "期刊编号")] 
		 public int? journalid{ get; set; } 

		 /// <summary>
		 /// 刊期id 
		 /// <summary>
		 [Display(Name = "刊期id")] 
		 public int? issue{ get; set; } 

		 /// <summary>
		 /// 中文标题 
		 /// <summary>
		 [Display(Name = "中文标题")] 
		 public string title{ get; set; } 

		 /// <summary>
		 /// 英文标题 
		 /// <summary>
		 [Display(Name = "英文标题")] 
		 public string entitle{ get; set; } 

		 /// <summary>
		 /// 中文名 
		 /// <summary>
		 [Display(Name = "中文名")] 
		 public string author{ get; set; } 

		 /// <summary>
		 /// 英文名 
		 /// <summary>
		 [Display(Name = "英文名")] 
		 public string enauthor{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// 期刊名称 
		 /// <summary>
		 [Display(Name = "期刊名称")] 
		 public string journal{ get; set; } 

		 /// <summary>
		 /// 期刊英文名 
		 /// <summary>
		 [Display(Name = "期刊英文名")] 
		 public string enjournal{ get; set; } 

		 /// <summary>
		 /// 关键词 
		 /// <summary>
		 [Display(Name = "关键词")] 
		 public string keyword{ get; set; } 

		 /// <summary>
		 /// 英文关键词 
		 /// <summary>
		 [Display(Name = "英文关键词")] 
		 public string enkeyword{ get; set; } 

		 /// <summary>
		 /// 中文摘要 
		 /// <summary>
		 [Display(Name = "中文摘要")] 
		 public string abstracts{ get; set; } 

		 /// <summary>
		 /// 英文摘要 
		 /// <summary>
		 [Display(Name = "英文摘要")] 
		 public string enabstracts{ get; set; } 

		 /// <summary>
		 /// 基金 
		 /// <summary>
		 [Display(Name = "基金")] 
		 public string foundation{ get; set; } 

		 /// <summary>
		 /// 发表时间 
		 /// <summary>
		 [Display(Name = "发表时间")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 研究层次 
		 /// <summary>
		 [Display(Name = "研究层次")] 
		 public string level{ get; set; } 

		 /// <summary>
		 /// 参考文献 
		 /// <summary>
		 [Display(Name = "参考文献")] 
		 public string reference{ get; set; } 

		 /// <summary>
		 /// 引用文献 
		 /// <summary>
		 [Display(Name = "引用文献")] 
		 public string quotes{ get; set; } 

		 /// <summary>
		 /// 出版年 
		 /// <summary>
		 [Display(Name = "出版年")] 
		 public DateTime? pubyear{ get; set; } 

		 /// <summary>
		 /// 国际标准刊号 
		 /// <summary>
		 [Display(Name = "国际标准刊号")] 
		 public string issn{ get; set; } 

		 /// <summary>
		 /// 国内统一刊号 
		 /// <summary>
		 [Display(Name = "国内统一刊号")] 
		 public string cn{ get; set; } 

		 /// <summary>
		 /// 页码 
		 /// <summary>
		 [Display(Name = "页码")] 
		 public int? pagenum{ get; set; } 

		 /// <summary>
		 /// 学科分类 
		 /// <summary>
		 [Display(Name = "学科分类")] 
		 public string course{ get; set; } 

		 /// <summary>
		 /// 中图分类号 
		 /// <summary>
		 [Display(Name = "中图分类号")] 
		 public string clc{ get; set; } 

		 /// <summary>
		 /// sci收录刊 
		 /// <summary>
		 [Display(Name = "sci收录刊")] 
		 public string sci{ get; set; } 

		 /// <summary>
		 /// ei收录刊 
		 /// <summary>
		 [Display(Name = "ei收录刊")] 
		 public string ei{ get; set; } 

		 /// <summary>
		 /// 核心期刊 
		 /// <summary>
		 [Display(Name = "核心期刊")] 
		 public string corejournal{ get; set; } 

		 /// <summary>
		 /// 主题 
		 /// <summary>
		 [Display(Name = "主题")] 
		 public string theme{ get; set; } 

		 /// <summary>
		 /// 语言 
		 /// <summary>
		 [Display(Name = "语言")] 
		 public int? language{ get; set; } 

		 /// <summary>
		 /// 出版单位 
		 /// <summary>
		 [Display(Name = "出版单位")] 
		 public string pubunit{ get; set; } 

		 /// <summary>
		 /// 正文快照 
		 /// <summary>
		 [Display(Name = "正文快照")] 
		 public string textpicture{ get; set; } 

		 /// <summary>
		 /// 全文路径 
		 /// <summary>
		 [Display(Name = "全文路径")] 
		 public string fullpath{ get; set; } 

		 /// <summary>
		 /// 通讯作者 
		 /// <summary>
		 [Display(Name = "通讯作者")] 
		 public string commauthor{ get; set; } 

		 /// <summary>
		 /// 数字对象唯一标识符 
		 /// <summary>
		 [Display(Name = "数字对象唯一标识符")] 
		 public string doi{ get; set; } 

		 /// <summary>
		 /// 被引用量 
		 /// <summary>
		 [Display(Name = "被引用量")] 
		 public int? quotenum{ get; set; } 

		 /// <summary>
		 /// 相似文献 
		 /// <summary>
		 [Display(Name = "相似文献")] 
		 public string similarissue{ get; set; } 

		 /// <summary>
		 /// 相关博文 
		 /// <summary>
		 [Display(Name = "相关博文")] 
		 public string relateblog{ get; set; } 

		 /// <summary>
		 /// 相关学者 
		 /// <summary>
		 [Display(Name = "相关学者")] 
		 public string relatescholars{ get; set; } 

		 /// <summary>
		 /// 相关检索词 
		 /// <summary>
		 [Display(Name = "相关检索词")] 
		 public string relateword{ get; set; } 

		 /// <summary>
		 /// 机标分类号 
		 /// <summary>
		 [Display(Name = "机标分类号")] 
		 public string mlcn{ get; set; } 

		 /// <summary>
		 /// 相关文献 
		 /// <summary>
		 [Display(Name = "相关文献")] 
		 public string relateissue{ get; set; } 

		 /// <summary>
		 /// 作者其他文献 
		 /// <summary>
		 [Display(Name = "作者其他文献")] 
		 public string authorissues{ get; set; } 

		 /// <summary>
		 /// 机构其他文献 
		 /// <summary>
		 [Display(Name = "机构其他文献")] 
		 public string institutionissue{ get; set; } 

		 /// <summary>
		 /// 同行关注文献 
		 /// <summary>
		 [Display(Name = "同行关注文献")] 
		 public string peerissue{ get; set; } 

		 /// <summary>
		 /// 图片 
		 /// <summary>
		 [Display(Name = "图片")] 
		 public string picture{ get; set; } 

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
			 self_journalarticle model = obj as self_journalarticle;
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
