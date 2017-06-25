using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 政策解读管理 self_policyinterpretation 
	 /// </summary>
	 public partial class self_policyinterpretation : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_policyinterpretation 
		 /// </summary>
		 public self_policyinterpretation() 
		 { }

		 /// <summary>
		 /// 构造函数 self_policyinterpretation 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_title">标题</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_type">解读类型1部门2专家3媒体</param> 
		 /// <param name="_source">来源</param> 
		 /// <param name="_pubdate">发布日期</param> 
		 /// <param name="_editor">编辑</param> 
		 /// <param name="_abstracts">摘要</param> 
		 /// <param name="_text">正文</param> 
		 /// <param name="_attach">附件</param> 
		 /// <param name="_sourceweb">来源网站</param> 
		 /// <param name="_picture">图片</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_isfull">是否全文0否1是</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_policyinterpretation(int? _seqid,string _title,string _subjectclass,int? _type,string _source,DateTime? _pubdate,string _editor,string _abstracts,string _text,string _attach,string _sourceweb,string _picture,int? _orgid,int? _isfull,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.title = _title;
			 this.subjectclass = _subjectclass;
			 this.type = _type;
			 this.source = _source;
			 this.pubdate = _pubdate;
			 this.editor = _editor;
			 this.abstracts = _abstracts;
			 this.text = _text;
			 this.attach = _attach;
			 this.sourceweb = _sourceweb;
			 this.picture = _picture;
			 this.orgid = _orgid;
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
		 /// 解读类型1部门2专家3媒体 
		 /// <summary>
		 [Display(Name = "解读类型1部门2专家3媒体")] 
		 public int? type{ get; set; } 

		 /// <summary>
		 /// 来源 
		 /// <summary>
		 [Display(Name = "来源")] 
		 public string source{ get; set; } 

		 /// <summary>
		 /// 发布日期 
		 /// <summary>
		 [Display(Name = "发布日期")] 
		 public DateTime? pubdate{ get; set; } 

		 /// <summary>
		 /// 编辑 
		 /// <summary>
		 [Display(Name = "编辑")] 
		 public string editor{ get; set; } 

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
		 /// 所属机构 
		 /// <summary>
		 [Display(Name = "所属机构")] 
		 public int? orgid{ get; set; } 

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
			 self_policyinterpretation model = obj as self_policyinterpretation;
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
