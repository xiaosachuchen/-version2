using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RIPSP.Model 
{
	 /// <summary>
	 /// 实体Model 专家管理 self_expert 
	 /// </summary>
	 public partial class self_expert : ICloneable 
	 {
		 /// <summary>
		 /// 构造函数 self_expert 
		 /// </summary>
		 public self_expert() 
		 { }

		 /// <summary>
		 /// 构造函数 self_expert 
		 /// </summary>
		 /// <param name="_seqid">编号</param> 
		 /// <param name="_userid">用户编号</param> 
		 /// <param name="_realname">姓名</param> 
		 /// <param name="_enname">英文名</param> 
		 /// <param name="_experttype">专家类型1书评人2教授</param> 
		 /// <param name="_subjectclass">主题分类</param> 
		 /// <param name="_birthday">出生日期</param> 
		 /// <param name="_background">学术背景</param> 
		 /// <param name="_work">工作背景</param> 
		 /// <param name="_voie_creuse">voie_creuse</param> 
		 /// <param name="_tel">电话</param> 
		 /// <param name="_email">邮箱</param> 
		 /// <param name="_website">网址</param> 
		 /// <param name="_country">国别</param> 
		 /// <param name="_areacode">地区</param> 
		 /// <param name="_office">地址</param> 
		 /// <param name="_research">研究领域</param> 
		 /// <param name="_rescenter">研究中心</param> 
		 /// <param name="_resinterest">研究兴趣</param> 
		 /// <param name="_resdynamic">研究动态</param> 
		 /// <param name="_bachelorscourse">本科课程</param> 
		 /// <param name="_mastercourse">硕士课程</param> 
		 /// <param name="_majorcworks">主要中文学术著作</param> 
		 /// <param name="_majoreworks">主要英文学术著作</param> 
		 /// <param name="_worknum">成果数</param> 
		 /// <param name="_enworknum">外文成果数</param> 
		 /// <param name="_translator">译者</param> 
		 /// <param name="_honor">获奖情况</param> 
		 /// <param name="_first">第一作者成果数</param> 
		 /// <param name="_quotenum">被引频次</param> 
		 /// <param name="_introduce">专家简介</param> 
		 /// <param name="_h_index">h指数</param> 
		 /// <param name="_g_index">g指数</param> 
		 /// <param name="_worklist">成果列表</param> 
		 /// <param name="_workscholar">合作学者</param> 
		 /// <param name="_workorg">合作机构</param> 
		 /// <param name="_firstpic">头像</param> 
		 /// <param name="_project">课题</param> 
		 /// <param name="_booklilst">著作从书</param> 
		 /// <param name="_journalist">期刊论文</param> 
		 /// <param name="_parttimejob">学术兼职</param> 
		 /// <param name="_socialwork">社会职务</param> 
		 /// <param name="_mainpoint">主要观点</param> 
		 /// <param name="_standpoint">立场评估</param> 
		 /// <param name="_analysis">书评分析</param> 
		 /// <param name="_points">总体评估</param> 
		 /// <param name="_appendixid">附录编号</param> 
		 /// <param name="_notes">注释</param> 
		 /// <param name="_orgid">所属机构</param> 
		 /// <param name="_hits">点击量</param> 
		 /// <param name="_createdtime">创建时间</param> 
		 /// <param name="_creatorid">创建人</param> 
		 /// <param name="_status">资源状态</param> 
		 public self_expert(int? _seqid,int? _userid,string _realname,string _enname,int? _experttype,string _subjectclass,DateTime? _birthday,string _background,string _work,string _voie_creuse,string _tel,string _email,string _website,string _country,string _areacode,string _office,string _research,string _rescenter,string _resinterest,string _resdynamic,string _bachelorscourse,string _mastercourse,string _majorcworks,string _majoreworks,int? _worknum,int? _enworknum,string _translator,string _honor,int? _first,int? _quotenum,string _introduce,string _h_index,string _g_index,string _worklist,string _workscholar,string _workorg,string _firstpic,string _project,string _booklilst,string _journalist,string _parttimejob,string _socialwork,string _mainpoint,string _standpoint,string _analysis,string _points,string _appendixid,string _notes,int? _orgid,int? _hits,DateTime? _createdtime,int? _creatorid,int? _status) 
		 {
			 this.seqid = _seqid;
			 this.userid = _userid;
			 this.realname = _realname;
			 this.enname = _enname;
			 this.experttype = _experttype;
			 this.subjectclass = _subjectclass;
			 this.birthday = _birthday;
			 this.background = _background;
			 this.work = _work;
			 this.voie_creuse = _voie_creuse;
			 this.tel = _tel;
			 this.email = _email;
			 this.website = _website;
			 this.country = _country;
			 this.areacode = _areacode;
			 this.office = _office;
			 this.research = _research;
			 this.rescenter = _rescenter;
			 this.resinterest = _resinterest;
			 this.resdynamic = _resdynamic;
			 this.bachelorscourse = _bachelorscourse;
			 this.mastercourse = _mastercourse;
			 this.majorcworks = _majorcworks;
			 this.majoreworks = _majoreworks;
			 this.worknum = _worknum;
			 this.enworknum = _enworknum;
			 this.translator = _translator;
			 this.honor = _honor;
			 this.first = _first;
			 this.quotenum = _quotenum;
			 this.introduce = _introduce;
			 this.h_index = _h_index;
			 this.g_index = _g_index;
			 this.worklist = _worklist;
			 this.workscholar = _workscholar;
			 this.workorg = _workorg;
			 this.firstpic = _firstpic;
			 this.project = _project;
			 this.booklilst = _booklilst;
			 this.journalist = _journalist;
			 this.parttimejob = _parttimejob;
			 this.socialwork = _socialwork;
			 this.mainpoint = _mainpoint;
			 this.standpoint = _standpoint;
			 this.analysis = _analysis;
			 this.points = _points;
			 this.appendixid = _appendixid;
			 this.notes = _notes;
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
		 /// 英文名 
		 /// <summary>
		 [Display(Name = "英文名")] 
		 public string enname{ get; set; } 

		 /// <summary>
		 /// 专家类型1书评人2教授 
		 /// <summary>
		 [Display(Name = "专家类型1书评人2教授")] 
		 public int? experttype{ get; set; } 

		 /// <summary>
		 /// 主题分类 
		 /// <summary>
		 [Display(Name = "主题分类")] 
		 public string subjectclass{ get; set; } 

		 /// <summary>
		 /// 出生日期 
		 /// <summary>
		 [Display(Name = "出生日期")] 
		 public DateTime? birthday{ get; set; } 

		 /// <summary>
		 /// 学术背景 
		 /// <summary>
		 [Display(Name = "学术背景")] 
		 public string background{ get; set; } 

		 /// <summary>
		 /// 工作背景 
		 /// <summary>
		 [Display(Name = "工作背景")] 
		 public string work{ get; set; } 

		 /// <summary>
		 /// voie_creuse 
		 /// <summary>
		 [Display(Name = "voie_creuse")] 
		 public string voie_creuse{ get; set; } 

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
		 /// 地址 
		 /// <summary>
		 [Display(Name = "地址")] 
		 public string office{ get; set; } 

		 /// <summary>
		 /// 研究领域 
		 /// <summary>
		 [Display(Name = "研究领域")] 
		 public string research{ get; set; } 

		 /// <summary>
		 /// 研究中心 
		 /// <summary>
		 [Display(Name = "研究中心")] 
		 public string rescenter{ get; set; } 

		 /// <summary>
		 /// 研究兴趣 
		 /// <summary>
		 [Display(Name = "研究兴趣")] 
		 public string resinterest{ get; set; } 

		 /// <summary>
		 /// 研究动态 
		 /// <summary>
		 [Display(Name = "研究动态")] 
		 public string resdynamic{ get; set; } 

		 /// <summary>
		 /// 本科课程 
		 /// <summary>
		 [Display(Name = "本科课程")] 
		 public string bachelorscourse{ get; set; } 

		 /// <summary>
		 /// 硕士课程 
		 /// <summary>
		 [Display(Name = "硕士课程")] 
		 public string mastercourse{ get; set; } 

		 /// <summary>
		 /// 主要中文学术著作 
		 /// <summary>
		 [Display(Name = "主要中文学术著作")] 
		 public string majorcworks{ get; set; } 

		 /// <summary>
		 /// 主要英文学术著作 
		 /// <summary>
		 [Display(Name = "主要英文学术著作")] 
		 public string majoreworks{ get; set; } 

		 /// <summary>
		 /// 成果数 
		 /// <summary>
		 [Display(Name = "成果数")] 
		 public int? worknum{ get; set; } 

		 /// <summary>
		 /// 外文成果数 
		 /// <summary>
		 [Display(Name = "外文成果数")] 
		 public int? enworknum{ get; set; } 

		 /// <summary>
		 /// 译者 
		 /// <summary>
		 [Display(Name = "译者")] 
		 public string translator{ get; set; } 

		 /// <summary>
		 /// 获奖情况 
		 /// <summary>
		 [Display(Name = "获奖情况")] 
		 public string honor{ get; set; } 

		 /// <summary>
		 /// 第一作者成果数 
		 /// <summary>
		 [Display(Name = "第一作者成果数")] 
		 public int? first{ get; set; } 

		 /// <summary>
		 /// 被引频次 
		 /// <summary>
		 [Display(Name = "被引频次")] 
		 public int? quotenum{ get; set; } 

		 /// <summary>
		 /// 专家简介 
		 /// <summary>
		 [Display(Name = "专家简介")] 
		 public string introduce{ get; set; } 

		 /// <summary>
		 /// h指数 
		 /// <summary>
		 [Display(Name = "h指数")] 
		 public string h_index{ get; set; } 

		 /// <summary>
		 /// g指数 
		 /// <summary>
		 [Display(Name = "g指数")] 
		 public string g_index{ get; set; } 

		 /// <summary>
		 /// 成果列表 
		 /// <summary>
		 [Display(Name = "成果列表")] 
		 public string worklist{ get; set; } 

		 /// <summary>
		 /// 合作学者 
		 /// <summary>
		 [Display(Name = "合作学者")] 
		 public string workscholar{ get; set; } 

		 /// <summary>
		 /// 合作机构 
		 /// <summary>
		 [Display(Name = "合作机构")] 
		 public string workorg{ get; set; } 

		 /// <summary>
		 /// 头像 
		 /// <summary>
		 [Display(Name = "头像")] 
		 public string firstpic{ get; set; } 

		 /// <summary>
		 /// 课题 
		 /// <summary>
		 [Display(Name = "课题")] 
		 public string project{ get; set; } 

		 /// <summary>
		 /// 著作从书 
		 /// <summary>
		 [Display(Name = "著作从书")] 
		 public string booklilst{ get; set; } 

		 /// <summary>
		 /// 期刊论文 
		 /// <summary>
		 [Display(Name = "期刊论文")] 
		 public string journalist{ get; set; } 

		 /// <summary>
		 /// 学术兼职 
		 /// <summary>
		 [Display(Name = "学术兼职")] 
		 public string parttimejob{ get; set; } 

		 /// <summary>
		 /// 社会职务 
		 /// <summary>
		 [Display(Name = "社会职务")] 
		 public string socialwork{ get; set; } 

		 /// <summary>
		 /// 主要观点 
		 /// <summary>
		 [Display(Name = "主要观点")] 
		 public string mainpoint{ get; set; } 

		 /// <summary>
		 /// 立场评估 
		 /// <summary>
		 [Display(Name = "立场评估")] 
		 public string standpoint{ get; set; } 

		 /// <summary>
		 /// 书评分析 
		 /// <summary>
		 [Display(Name = "书评分析")] 
		 public string analysis{ get; set; } 

		 /// <summary>
		 /// 总体评估 
		 /// <summary>
		 [Display(Name = "总体评估")] 
		 public string points{ get; set; } 

		 /// <summary>
		 /// 附录编号 
		 /// <summary>
		 [Display(Name = "附录编号")] 
		 public string appendixid{ get; set; } 

		 /// <summary>
		 /// 注释 
		 /// <summary>
		 [Display(Name = "注释")] 
		 public string notes{ get; set; } 

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
			 self_expert model = obj as self_expert;
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
