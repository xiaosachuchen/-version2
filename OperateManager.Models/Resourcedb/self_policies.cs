using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 政策法规管理 self_policies 
	 /// </summary>
	 public partial class self_policies : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_policies 
		 /// </summary>
		 public self_policies() 
		 { }

		 /// <summary>
		 /// 构造函数 self_policies 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_title">标题</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_indexno">索引号</param> 
		 /// <param name="_subjectwords">主题词</param> 
		 /// <param name="_disagency">发文机关</param> 
		 /// <param name="_disnum">发文字号</param> 
		 /// <param name="_writedate">成文日期</param> 
		 /// <param name="_pubdate">发布日期</param> 
		 /// <param name="_impledate">实施日期</param> 
		 /// <param name="_level">公文级别</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_keywords">关键字</param> 
		 /// <param name="_language">语种</param> 
		 /// <param name="_genre">体裁</param> 
		 /// <param name="_abstracts">摘要</param> 
		 /// <param name="_text">正文</param> 
		 /// <param name="_attach">附件</param> 
		 /// <param name="_source">政策来源网站明</param> 
		 /// <param name="_sourceweb">来源网站</param> 
		 /// <param name="_picture">图片</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_policies(int? _seqid,string _title,string _subjectclass,string _indexno,string _subjectwords,string _disagency,string _disnum,DateTime? _writedate,DateTime? _pubdate,DateTime? _impledate,int? _level,int? _orgid,string _keywords,int? _language,string _genre,string _abstracts,string _text,string _attach,string _source,string _sourceweb,string _picture,int? _isfull,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.title = _title;
			 this.subjectclass = _subjectclass;
			 this.indexno = _indexno;
			 this.subjectwords = _subjectwords;
			 this.disagency = _disagency;
			 this.disnum = _disnum;
			 this.writedate = _writedate;
			 this.pubdate = _pubdate;
			 this.impledate = _impledate;
			 this.level = _level;
			 this.orgid = _orgid;
			 this.keywords = _keywords;
			 this.language = _language;
			 this.genre = _genre;
			 this.abstracts = _abstracts;
			 this.text = _text;
			 this.attach = _attach;
			 this.source = _source;
			 this.sourceweb = _sourceweb;
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
		 /// 标题 
		 /// <summary>
		 [Display(Name = "标题")] 
		 public string title{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 索引号 
		 /// <summary>
		 [Display(Name = "索引号")] 
		 public string indexno{ get; set; } 

		 /// <summary>
		 /// 主题词 
		 /// <summary>
		 [Display(Name = "主题词")] 
		 public string subjectwords{ get; set; } 

		 /// <summary>
		 /// 发文机关 
		 /// <summary>
		 [Display(Name = "发文机关")] 
		 public string disagency{ get; set; } 

		 /// <summary>
		 /// 发文字号 
		 /// <summary>
		 [Display(Name = "发文字号")] 
		 public string disnum{ get; set; } 

		 /// <summary>
		 /// 成文日期 
		 /// <summary>
		 [Display(Name = "成文日期")] 
		 public DateTime? writedate{ get; set; } 

		 /// <summary>
		 /// 发布日期 
		 /// <summary>
		 [Display(Name = "发布日期")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 实施日期 
		 /// <summary>
		 [Display(Name = "实施日期")] 
		 public DateTime? impledate{ get; set; } 

		 /// <summary>
		 /// 公文级别 
		 /// <summary>
		 [Display(Name = "公文级别")] 
		 public int? level{ get; set; } 

		 /// <summary>
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

		 /// <summary>
		 /// 关键字 
		 /// <summary>
		 [Display(Name = "关键字")] 
		 public string keywords{ get; set; } 

		 /// <summary>
		 /// 语种 
		 /// <summary>
		 [Display(Name = "语种")] 
		 public int? language{ get; set; } 

		 /// <summary>
		 /// 体裁 
		 /// <summary>
		 [Display(Name = "体裁")] 
		 public string genre{ get; set; } 

		 /// <summary>
		 /// 摘要 
		 /// <summary>
		 [Display(Name = "摘要")] 
		 public string abstracts{ get; set; } 

		 /// <summary>
		 /// 正文 
		 /// <summary>
		 [Display(Name = "正文")] 
		 public string text{ get; set; } 

		 /// <summary>
		 /// 附件 
		 /// <summary>
		 [Display(Name = "附件")] 
		 public string attach{ get; set; } 

		 /// <summary>
		 /// 政策来源网站明 
		 /// <summary>
		 [Display(Name = "政策来源网站明")] 
		 public string source{ get; set; } 

		 /// <summary>
		 /// 来源网站 
		 /// <summary>
		 [Display(Name = "来源网站")] 
		 public string sourceweb{ get; set; } 

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
			 self_policies model = obj as self_policies;
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
