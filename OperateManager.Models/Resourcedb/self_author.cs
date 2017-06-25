using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 作者管理 self_author 
	 /// </summary>
	 public partial class self_author : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_author 
		 /// </summary>
		 public self_author() 
		 { }

		 /// <summary>
		 /// 构造函数 self_author 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_userid">用户编号</param> 
		 /// <param name="_realname">姓名</param> 
		 /// <param name="_nickname">笔名</param> 
		 /// <param name="_authortitle">头衔</param> 
		 /// <param name="_tel">电话</param> 
		 /// <param name="_email">邮箱</param> 
		 /// <param name="_website">网址</param> 
		 /// <param name="_institution">作者机构</param> 
		 /// <param name="_introduce">作者简介</param> 
		 /// <param name="_firstpic">头像</param> 
		 /// <param name="_country">国别</param> 
		 /// <param name="_areacode">地区</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_author(int? _seqid,int? _userid,string _realname,string _nickname,string _authortitle,string _tel,string _email,string _website,string _institution,string _introduce,string _firstpic,string _country,string _areacode,int? _orgid,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.userid = _userid;
			 this.realname = _realname;
			 this.nickname = _nickname;
			 this.authortitle = _authortitle;
			 this.tel = _tel;
			 this.email = _email;
			 this.website = _website;
			 this.institution = _institution;
			 this.introduce = _introduce;
			 this.firstpic = _firstpic;
			 this.country = _country;
			 this.areacode = _areacode;
			 this.orgid = _orgid;
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
		 /// 用户编号 
		 /// <summary>
		 [Display(Name = "用户编号")] 
		 public int? userid{ get; set; } 

		 /// <summary>
		 /// 姓名 
		 /// <summary>
		 [Display(Name = "姓名")] 
		 public string realname{ get; set; } 

		 /// <summary>
		 /// 笔名 
		 /// <summary>
		 [Display(Name = "笔名")] 
		 public string nickname{ get; set; } 

		 /// <summary>
		 /// 头衔 
		 /// <summary>
		 [Display(Name = "头衔")] 
		 public string authortitle{ get; set; } 

		 /// <summary>
		 /// 电话 
		 /// <summary>
		 [Display(Name = "电话")] 
		 public string tel{ get; set; } 

		 /// <summary>
		 /// 邮箱 
		 /// <summary>
		 [Display(Name = "邮箱")] 
		 public string email{ get; set; } 

		 /// <summary>
		 /// 网址 
		 /// <summary>
		 [Display(Name = "网址")] 
		 public string website{ get; set; } 

		 /// <summary>
		 /// 作者机构 
		 /// <summary>
		 [Display(Name = "作者机构")] 
		 public string institution{ get; set; } 

		 /// <summary>
		 /// 作者简介 
		 /// <summary>
		 [Display(Name = "作者简介")] 
		 public string introduce{ get; set; } 

		 /// <summary>
		 /// 头像 
		 /// <summary>
		 [Display(Name = "头像")] 
		 public string firstpic{ get; set; } 

		 /// <summary>
		 /// 国别 
		 /// <summary>
		 [Display(Name = "国别")] 
		 public string country{ get; set; } 

		 /// <summary>
		 /// 地区 
		 /// <summary>
		 [Display(Name = "地区")] 
		 public string areacode{ get; set; } 

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
			 self_author model = obj as self_author;
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
