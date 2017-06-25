using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 期刊管理 self_journal 
	 /// </summary>
	 public partial class self_journal : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_journal 
		 /// </summary>
		 public self_journal() 
		 { }

		 /// <summary>
		 /// 构造函数 self_journal 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_journalname">中文刊名</param> 
		 /// <param name="_enname">英文刊名</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_directordep">主管单位</param> 
		 /// <param name="_sponsordep">主办单位</param> 
		 /// <param name="_pubperiod">出版周期</param> 
		 /// <param name="_pubyear">出版年</param> 
		 /// <param name="_issn">国际标准刊号</param> 
		 /// <param name="_cn">国内统一刊号</param> 
		 /// <param name="_pubplace">出版地</param> 
		 /// <param name="_language">语种</param> 
		 /// <param name="_size">开本</param> 
		 /// <param name="_sendcode">邮发代号</param> 
		 /// <param name="_foundtime">创刊时间</param> 
		 /// <param name="_editor">主编</param> 
		 /// <param name="_address">地址</param> 
		 /// <param name="_postcode">邮政编码</param> 
		 /// <param name="_phone">电话</param> 
		 /// <param name="_email">电子邮件</param> 
		 /// <param name="_website">网址</param> 
		 /// <param name="_sci">sci收录刊</param> 
		 /// <param name="_ei">ei收录刊</param> 
		 /// <param name="_corejournal">核心期刊</param> 
		 /// <param name="_collectionintro">数据库收录情况</param> 
		 /// <param name="_honor">期刊荣誉</param> 
		 /// <param name="_introduction">期刊简介</param> 
		 /// <param name="_articlesnum">出版文献量</param> 
		 /// <param name="_quotenum">被引用量</param> 
		 /// <param name="_coverpath">封面</param> 
		 /// <param name="_columns">主要栏目</param> 
		 /// <param name="_h_index">h指数</param> 
		 /// <param name="_posttopic">发文主题</param> 
		 /// <param name="_postarea">发文领域</param> 
		 /// <param name="_includeyear">收录期刊</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_journal(int? _seqid,string _journalname,string _enname,string _subjectclass,string _directordep,string _sponsordep,string _pubperiod,DateTime? _pubyear,string _issn,string _cn,string _pubplace,int? _language,string _size,string _sendcode,DateTime? _foundtime,string _editor,string _address,string _postcode,string _phone,string _email,string _website,string _sci,string _ei,string _corejournal,string _collectionintro,string _honor,string _introduction,int? _articlesnum,int? _quotenum,string _coverpath,string _columns,string _h_index,string _posttopic,string _postarea,string _includeyear,int? _hits,int? _orgid,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.journalname = _journalname;
			 this.enname = _enname;
			 this.subjectclass = _subjectclass;
			 this.directordep = _directordep;
			 this.sponsordep = _sponsordep;
			 this.pubperiod = _pubperiod;
			 this.pubyear = _pubyear;
			 this.issn = _issn;
			 this.cn = _cn;
			 this.pubplace = _pubplace;
			 this.language = _language;
			 this.size = _size;
			 this.sendcode = _sendcode;
			 this.foundtime = _foundtime;
			 this.editor = _editor;
			 this.address = _address;
			 this.postcode = _postcode;
			 this.phone = _phone;
			 this.email = _email;
			 this.website = _website;
			 this.sci = _sci;
			 this.ei = _ei;
			 this.corejournal = _corejournal;
			 this.collectionintro = _collectionintro;
			 this.honor = _honor;
			 this.introduction = _introduction;
			 this.articlesnum = _articlesnum;
			 this.quotenum = _quotenum;
			 this.coverpath = _coverpath;
			 this.columns = _columns;
			 this.h_index = _h_index;
			 this.posttopic = _posttopic;
			 this.postarea = _postarea;
			 this.includeyear = _includeyear;
			 this.hits = _hits;
			 this.orgid = _orgid;
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
		 /// 中文刊名 
		 /// <summary>
		 [Display(Name = "中文刊名")] 
		 public string journalname{ get; set; } 

		 /// <summary>
		 /// 英文刊名 
		 /// <summary>
		 [Display(Name = "英文刊名")] 
		 public string enname{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 主管单位 
		 /// <summary>
		 [Display(Name = "主管单位")] 
		 public string directordep{ get; set; } 

		 /// <summary>
		 /// 主办单位 
		 /// <summary>
		 [Display(Name = "主办单位")] 
		 public string sponsordep{ get; set; } 

		 /// <summary>
		 /// 出版周期 
		 /// <summary>
		 [Display(Name = "出版周期")] 
		 public string pubperiod{ get; set; } 

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
		 /// 出版地 
		 /// <summary>
		 [Display(Name = "出版地")] 
		 public string pubplace{ get; set; } 

		 /// <summary>
		 /// 语种 
		 /// <summary>
		 [Display(Name = "语种")] 
		 public int? language{ get; set; } 

		 /// <summary>
		 /// 开本 
		 /// <summary>
		 [Display(Name = "开本")] 
		 public string size{ get; set; } 

		 /// <summary>
		 /// 邮发代号 
		 /// <summary>
		 [Display(Name = "邮发代号")] 
		 public string sendcode{ get; set; } 

		 /// <summary>
		 /// 创刊时间 
		 /// <summary>
		 [Display(Name = "创刊时间")] 
		 public DateTime? foundtime{ get; set; } 

		 /// <summary>
		 /// 主编 
		 /// <summary>
		 [Display(Name = "主编")] 
		 public string editor{ get; set; } 

		 /// <summary>
		 /// 地址 
		 /// <summary>
		 [Display(Name = "地址")] 
		 public string address{ get; set; } 

		 /// <summary>
		 /// 邮政编码 
		 /// <summary>
		 [Display(Name = "邮政编码")] 
		 public string postcode{ get; set; } 

		 /// <summary>
		 /// 电话 
		 /// <summary>
		 [Display(Name = "电话")] 
		 public string phone{ get; set; } 

		 /// <summary>
		 /// 电子邮件 
		 /// <summary>
		 [Display(Name = "电子邮件")] 
		 public string email{ get; set; } 

		 /// <summary>
		 /// 网址 
		 /// <summary>
		 [Display(Name = "网址")] 
		 public string website{ get; set; } 

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
		 /// 数据库收录情况 
		 /// <summary>
		 [Display(Name = "数据库收录情况")] 
		 public string collectionintro{ get; set; } 

		 /// <summary>
		 /// 期刊荣誉 
		 /// <summary>
		 [Display(Name = "期刊荣誉")] 
		 public string honor{ get; set; } 

		 /// <summary>
		 /// 期刊简介 
		 /// <summary>
		 [Display(Name = "期刊简介")] 
		 public string introduction{ get; set; } 

		 /// <summary>
		 /// 出版文献量 
		 /// <summary>
		 [Display(Name = "出版文献量")] 
		 public int? articlesnum{ get; set; } 

		 /// <summary>
		 /// 被引用量 
		 /// <summary>
		 [Display(Name = "被引用量")] 
		 public int? quotenum{ get; set; } 

		 /// <summary>
		 /// 封面 
		 /// <summary>
		 [Display(Name = "封面")] 
		 public string coverpath{ get; set; } 

		 /// <summary>
		 /// 主要栏目 
		 /// <summary>
		 [Display(Name = "主要栏目")] 
		 public string columns{ get; set; } 

		 /// <summary>
		 /// h指数 
		 /// <summary>
		 [Display(Name = "h指数")] 
		 public string h_index{ get; set; } 

		 /// <summary>
		 /// 发文主题 
		 /// <summary>
		 [Display(Name = "发文主题")] 
		 public string posttopic{ get; set; } 

		 /// <summary>
		 /// 发文领域 
		 /// <summary>
		 [Display(Name = "发文领域")] 
		 public string postarea{ get; set; } 

		 /// <summary>
		 /// 收录期刊 
		 /// <summary>
		 [Display(Name = "收录期刊")] 
		 public string includeyear{ get; set; } 

		 /// <summary>
		 /// 点击量 
		 /// <summary>
		 [Display(Name = "点击量")] 
		 public int? hits{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

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
			 self_journal model = obj as self_journal;
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
