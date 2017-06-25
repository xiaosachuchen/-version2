using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 学位论文管理 self_degreethesis 
	 /// </summary>
	 public partial class self_degreethesis : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_degreethesis 
		 /// </summary>
		 public self_degreethesis() 
		 { }

		 /// <summary>
		 /// 构造函数 self_degreethesis 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_title">中文标题</param> 
		 /// <param name="_entitle">英文标题</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_author">作者</param> 
		 /// <param name="_institution">作者机构</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_tutor">导师</param> 
		 /// <param name="_catalog">中文目录</param> 
		 /// <param name="_encatalog">英文目录</param> 
		 /// <param name="_major">学科专业</param> 
		 /// <param name="_majorclass">学科分类</param> 
		 /// <param name="_degree">学位名称</param> 
		 /// <param name="_degreeyear">学位年度</param> 
		 /// <param name="_clc">中图分类号</param> 
		 /// <param name="_graduateschool">学位授予单位</param> 
		 /// <param name="_graduatedate">学位授予日期</param> 
		 /// <param name="_pubdate">发表时间</param> 
		 /// <param name="_keyword">关键词</param> 
		 /// <param name="_enkeyword">英文关键词</param> 
		 /// <param name="_abstracts">中文摘要</param> 
		 /// <param name="_enabstracts">英文摘要</param> 
		 /// <param name="_foundation">基金</param> 
		 /// <param name="_reference">参考文献</param> 
		 /// <param name="_quotes">引用文献</param> 
		 /// <param name="_tutorintro">导师简介</param> 
		 /// <param name="_pagenum">页数</param> 
		 /// <param name="_language">语言</param> 
		 /// <param name="_doi">数字对象唯一标识符</param> 
		 /// <param name="_theme">主题词</param> 
		 /// <param name="_download">下载次数</param> 
		 /// <param name="_quotenum">引用次数</param> 
		 /// <param name="_similartutorthesis">相同导师文献</param> 
		 /// <param name="_fullpath">全文路径</param> 
		 /// <param name="_picture">图片</param> 
		 /// <param name="_similarissue">相似文献</param> 
		 /// <param name="_relateblog">相关博文</param> 
		 /// <param name="_relatescholars">相关学者</param> 
		 /// <param name="_relateword">相关检索词</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_degreethesis(int? _seqid,string _title,string _entitle,string _subjectclass,int? _author,string _institution,int? _orgid,string _tutor,string _catalog,string _encatalog,string _major,string _majorclass,string _degree,DateTime? _degreeyear,string _clc,string _graduateschool,DateTime? _graduatedate,DateTime? _pubdate,string _keyword,string _enkeyword,string _abstracts,string _enabstracts,string _foundation,string _reference,string _quotes,string _tutorintro,int? _pagenum,int? _language,string _doi,string _theme,int? _download,int? _quotenum,string _similartutorthesis,string _fullpath,string _picture,string _similarissue,string _relateblog,string _relatescholars,string _relateword,int? _isfull,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.title = _title;
			 this.entitle = _entitle;
			 this.subjectclass = _subjectclass;
			 this.author = _author;
			 this.institution = _institution;
			 this.orgid = _orgid;
			 this.tutor = _tutor;
			 this.catalog = _catalog;
			 this.encatalog = _encatalog;
			 this.major = _major;
			 this.majorclass = _majorclass;
			 this.degree = _degree;
			 this.degreeyear = _degreeyear;
			 this.clc = _clc;
			 this.graduateschool = _graduateschool;
			 this.graduatedate = _graduatedate;
			 this.pubdate = _pubdate;
			 this.keyword = _keyword;
			 this.enkeyword = _enkeyword;
			 this.abstracts = _abstracts;
			 this.enabstracts = _enabstracts;
			 this.foundation = _foundation;
			 this.reference = _reference;
			 this.quotes = _quotes;
			 this.tutorintro = _tutorintro;
			 this.pagenum = _pagenum;
			 this.language = _language;
			 this.doi = _doi;
			 this.theme = _theme;
			 this.download = _download;
			 this.quotenum = _quotenum;
			 this.similartutorthesis = _similartutorthesis;
			 this.fullpath = _fullpath;
			 this.picture = _picture;
			 this.similarissue = _similarissue;
			 this.relateblog = _relateblog;
			 this.relatescholars = _relatescholars;
			 this.relateword = _relateword;
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
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 作者 
		 /// <summary>
		 [Display(Name = "作者")] 
		 public int? author{ get; set; } 

		 /// <summary>
		 /// 作者机构 
		 /// <summary>
		 [Display(Name = "作者机构")] 
		 public string institution{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// 导师 
		 /// <summary>
		 [Display(Name = "导师")] 
		 public string tutor{ get; set; } 

		 /// <summary>
		 /// 中文目录 
		 /// <summary>
		 [Display(Name = "中文目录")] 
		 public string catalog{ get; set; } 

		 /// <summary>
		 /// 英文目录 
		 /// <summary>
		 [Display(Name = "英文目录")] 
		 public string encatalog{ get; set; } 

		 /// <summary>
		 /// 学科专业 
		 /// <summary>
		 [Display(Name = "学科专业")] 
		 public string major{ get; set; } 

		 /// <summary>
		 /// 学科分类 
		 /// <summary>
		 [Display(Name = "学科分类")] 
		 public string majorclass{ get; set; } 

		 /// <summary>
		 /// 学位名称 
		 /// <summary>
		 [Display(Name = "学位名称")] 
		 public string degree{ get; set; } 

		 /// <summary>
		 /// 学位年度 
		 /// <summary>
		 [Display(Name = "学位年度")] 
		 public DateTime? degreeyear{ get; set; } 

		 /// <summary>
		 /// 中图分类号 
		 /// <summary>
		 [Display(Name = "中图分类号")] 
		 public string clc{ get; set; } 

		 /// <summary>
		 /// 学位授予单位 
		 /// <summary>
		 [Display(Name = "学位授予单位")] 
		 public string graduateschool{ get; set; } 

		 /// <summary>
		 /// 学位授予日期 
		 /// <summary>
		 [Display(Name = "学位授予日期")] 
		 public DateTime? graduatedate{ get; set; } 

		 /// <summary>
		 /// 发表时间 
		 /// <summary>
		 [Display(Name = "发表时间")] 
		 public DateTime? pubdate{ get; set; } 

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
		 /// 导师简介 
		 /// <summary>
		 [Display(Name = "导师简介")] 
		 public string tutorintro{ get; set; } 

		 /// <summary>
		 /// 页数 
		 /// <summary>
		 [Display(Name = "页数")] 
		 public int? pagenum{ get; set; } 

		 /// <summary>
		 /// 语言 
		 /// <summary>
		 [Display(Name = "语言")] 
		 public int? language{ get; set; } 

		 /// <summary>
		 /// 数字对象唯一标识符 
		 /// <summary>
		 [Display(Name = "数字对象唯一标识符")] 
		 public string doi{ get; set; } 

		 /// <summary>
		 /// 主题词 
		 /// <summary>
		 [Display(Name = "主题词")] 
		 public string theme{ get; set; } 

		 /// <summary>
		 /// 下载次数 
		 /// <summary>
		 [Display(Name = "下载次数")] 
		 public int? download{ get; set; } 

		 /// <summary>
		 /// 引用次数 
		 /// <summary>
		 [Display(Name = "引用次数")] 
		 public int? quotenum{ get; set; } 

		 /// <summary>
		 /// 相同导师文献 
		 /// <summary>
		 [Display(Name = "相同导师文献")] 
		 public string similartutorthesis{ get; set; } 

		 /// <summary>
		 /// 全文路径 
		 /// <summary>
		 [Display(Name = "全文路径")] 
		 public string fullpath{ get; set; } 

		 /// <summary>
		 /// 图片 
		 /// <summary>
		 [Display(Name = "图片")] 
		 public string picture{ get; set; } 

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
			 self_degreethesis model = obj as self_degreethesis;
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
