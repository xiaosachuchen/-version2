using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 出版社管理 self_press 
	 /// </summary>
	 public partial class self_press : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_press 
		 /// </summary>
		 public self_press() 
		 { }

		 /// <summary>
		 /// 构造函数 self_press 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_pressname">出版社名称</param> 
		 /// <param name="_logo">logo</param> 
		 /// <param name="_areacode">地区</param> 
		 /// <param name="_address">地址</param> 
		 /// <param name="_contacts">联系人</param> 
		 /// <param name="_telphone">电话</param> 
		 /// <param name="_fax">传真</param> 
		 /// <param name="_website">网址</param> 
		 /// <param name="_email">邮箱</param> 
		 /// <param name="_postcode">邮政编码</param> 
		 /// <param name="_createddate">成立时间</param> 
		 /// <param name="_introduce">详细介绍</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_press(int? _seqid,string _pressname,string _logo,string _areacode,string _address,string _contacts,string _telphone,string _fax,string _website,string _email,string _postcode,DateTime? _createddate,string _introduce,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.pressname = _pressname;
			 this.logo = _logo;
			 this.areacode = _areacode;
			 this.address = _address;
			 this.contacts = _contacts;
			 this.telphone = _telphone;
			 this.fax = _fax;
			 this.website = _website;
			 this.email = _email;
			 this.postcode = _postcode;
			 this.createddate = _createddate;
			 this.introduce = _introduce;
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
		 /// 出版社名称 
		 /// <summary>
		 [Display(Name = "出版社名称")] 
		 public string pressname{ get; set; } 

		 /// <summary>
		 /// logo 
		 /// <summary>
		 [Display(Name = "logo")] 
		 public string logo{ get; set; } 

		 /// <summary>
		 /// 地区 
		 /// <summary>
		 [Display(Name = "地区")] 
		 public string areacode{ get; set; } 

		 /// <summary>
		 /// 地址 
		 /// <summary>
		 [Display(Name = "地址")] 
		 public string address{ get; set; } 

		 /// <summary>
		 /// 联系人 
		 /// <summary>
		 [Display(Name = "联系人")] 
		 public string contacts{ get; set; } 

		 /// <summary>
		 /// 电话 
		 /// <summary>
		 [Display(Name = "电话")] 
		 public string telphone{ get; set; } 

		 /// <summary>
		 /// 传真 
		 /// <summary>
		 [Display(Name = "传真")] 
		 public string fax{ get; set; } 

		 /// <summary>
		 /// 网址 
		 /// <summary>
		 [Display(Name = "网址")] 
		 public string website{ get; set; } 

		 /// <summary>
		 /// 邮箱 
		 /// <summary>
		 [Display(Name = "邮箱")] 
		 public string email{ get; set; } 

		 /// <summary>
		 /// 邮政编码 
		 /// <summary>
		 [Display(Name = "邮政编码")] 
		 public string postcode{ get; set; } 

		 /// <summary>
		 /// 成立时间 
		 /// <summary>
		 [Display(Name = "成立时间")] 
		 public DateTime? createddate{ get; set; } 

		 /// <summary>
		 /// 详细介绍 
		 /// <summary>
		 [Display(Name = "详细介绍")] 
		 public string introduce{ get; set; } 

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
			 self_press model = obj as self_press;
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
