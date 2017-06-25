using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 章节管理 self_chapter 
	 /// </summary>
	 public partial class self_chapter : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_chapter 
		 /// </summary>
		 public self_chapter() 
		 { }

		 /// <summary>
		 /// 构造函数 self_chapter 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_booknum">图书编号</param> 
		 /// <param name="_chapternum">章节编号</param> 
		 /// <param name="_chaptername">章节名称</param> 
		 /// <param name="_chaptersort">章节次序</param> 
		 /// <param name="_startpage">章节起始页码</param> 
		 /// <param name="_endpage">章节终止页码</param> 
		 /// <param name="_author">章节作者</param> 
		 /// <param name="_reference">章节参考文献</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_chapter(int? _seqid,string _booknum,string _chapternum,string _chaptername,string _chaptersort,int? _startpage,int? _endpage,int? _author,string _reference,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.booknum = _booknum;
			 this.chapternum = _chapternum;
			 this.chaptername = _chaptername;
			 this.chaptersort = _chaptersort;
			 this.startpage = _startpage;
			 this.endpage = _endpage;
			 this.author = _author;
			 this.reference = _reference;
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
		 /// 图书编号 
		 /// <summary>
		 [Display(Name = "图书编号")] 
		 public string booknum{ get; set; } 

		 /// <summary>
		 /// 章节编号 
		 /// <summary>
		 [Display(Name = "章节编号")] 
		 public string chapternum{ get; set; } 

		 /// <summary>
		 /// 章节名称 
		 /// <summary>
		 [Display(Name = "章节名称")] 
		 public string chaptername{ get; set; } 

		 /// <summary>
		 /// 章节次序 
		 /// <summary>
		 [Display(Name = "章节次序")] 
		 public string chaptersort{ get; set; } 

		 /// <summary>
		 /// 章节起始页码 
		 /// <summary>
		 [Display(Name = "章节起始页码")] 
		 public int? startpage{ get; set; } 

		 /// <summary>
		 /// 章节终止页码 
		 /// <summary>
		 [Display(Name = "章节终止页码")] 
		 public int? endpage{ get; set; } 

		 /// <summary>
		 /// 章节作者 
		 /// <summary>
		 [Display(Name = "章节作者")] 
		 public int? author{ get; set; } 

		 /// <summary>
		 /// 章节参考文献 
		 /// <summary>
		 [Display(Name = "章节参考文献")] 
		 public string reference{ get; set; } 

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
			 self_chapter model = obj as self_chapter;
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
