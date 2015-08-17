using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class editor : System.Web.UI.Page
    {
        public string cont = string.Empty;
        wongtsengDB dbkit = new wongtsengDB();
        static int no = -1;
        enum nowtype { view, edit, add };
        static string edittype = nowtype.view.ToString();    //标记当前的操作类型

        protected void Page_Load(object sender, EventArgs e)
        {
         
            //cont = "<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">时间：</span><span>2013</span><span style=\"font-family:宋体;\">年</span><span>4</span><span style=\"font-family:宋体;\">月</span><span>11</span><span style=\"font-family:宋体;\">日</span> <span style=\"font-family:宋体;\">上午</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">人员：</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">环保局：叶书记、王支、柳科长、郝元、于站长、朱主任等</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">外部专家：东南大学徐教授、公安姚主任</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">理工：王真、朱旭光、吴永芬</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">（一）郝元主持会议，介绍项目背景</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">（二）理工介绍环保局云计算平台数据中心一期相关规范和标准建设工作</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">（三）专家发表意见与交流讨论</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>1</span><span style=\"font-family:宋体;\">、于站长</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">数据中心的标准与规范的建设是从全局考虑信息化系统的建设要求，意义重大，也有很好的示范作用；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;\">•</span><span style=\"color:red;\"> </span><span style=\"font-family:宋体;color:red;\">从信息层次的划分来看，包括</span><span style=\"color:red;\">100</span><span style=\"font-family:宋体;color:red;\">多页的数据字典，还不够清晰，主要还是基于目前工作和业务划分来做的，比如污染源应属于环境管理；辐射也涉及污染源；降尘降水属于大气等等，对信息层次的梳理需要业务人员参与；————分类重新梳理</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">成果主要是针对目前的情况，对未来的需求描述较少；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">污染源生命周期管理，要形成整体信息，可以统一查询和应用，不宜太分散；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:#C00000;background:yellow;\">•</span><span style=\"color:#C00000;background:yellow;\"> </span><span style=\"font-family:宋体;color:#C00000;background:yellow;\">环境质量相关数据表格个别地方和实际情况不一致，比如对某一次监测一般只有一个监测值，没有最大最小值；——用现有的真实入库数据</span><span style=\"color:#C00000;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:#00B050;\">•</span><span style=\"color:#00B050;\"> </span><span style=\"font-family:宋体;color:#00B050;\">应对数据库表及字段的命名应建立一定的规范——已有</span><span style=\"color:#00B050;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;background:yellow;\">•</span><span style=\"color:red;background:yellow;\"> </span><span style=\"font-family:宋体;color:red;background:yellow;\">基础对象模块，比如污染源的基础对象</span><span style=\"color:red;background:yellow;\">(</span><span style=\"font-family:宋体;color:red;background:yellow;\">具体不太理解</span><span style=\"color:red;background:yellow;\">)</span><span style=\"font-family:宋体;color:red;background:yellow;\">——加入污染源基本信息</span><span style=\"color:red;background:yellow;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;background:yellow;\">•</span><span style=\"color:red;background:yellow;\"> </span><span style=\"font-family:宋体;color:red;background:yellow;\">数据中心并不是对现有所有系统的所有信息进行汇集，而应是有选择的抽取和清洗；——尽量使用现有的</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">另外也应考虑区县和外部单位的有关需求；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>2</span><span style=\"font-family:宋体;\">、机动车：</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:#00B050;\">•</span><span style=\"color:#00B050;\"> </span><span style=\"font-family:宋体;color:#00B050;\">主要是机动车信息方面还需要和业务科室对接，我们也在梳理我们自己的数据结构————已体现</span><span style=\"color:#00B050;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>3</span><span style=\"font-family:宋体;\">、柳科长</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">我代表业务部门也就是用户，赞同之前几位同志的意见，</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">我们建设数据中心最开始的构思是数据中心必须建设，应尽快出台相关标准，固化下来再逐步完善和升级；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">各部门数据不互通不共享是目前面临的主要困难；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">一期建设不应太宏观，首先做到吧所有系统对决策有价值的数据都进行汇总，对原有数据进行分析，在对数据的框架进行设计最终形成规范，不能怕失误，尽可能多的收集数据；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">数据中心的主要作用是为领导机关提供决策依据，为所有系统的规范提供依据，不涉及业务工作，只做数据的抽取和展现；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>4</span><span style=\"font-family:宋体;\">、公安姚主任</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">首先数据中心的建设工作很有意义，我们公安也在做数据中心的建设工作；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">数据中心应在原有数据的基础上发展，而不是另起炉灶；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;\">•</span><span style=\"color:red;\"> </span><span style=\"font-family:宋体;color:red;\">从数据的分类上，我们公安系统划分为业务生产库、综合资源库、专题应用库三个层次，业务生产库的数据经抽取、清洗，形成综合资源库，专题应用库是对综合资源库的整合和运用。另外为了解决数据关联问题，还建立了关联库；——分类</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;\">•</span><span style=\"color:red;\"> </span><span style=\"font-family:宋体;color:red;\">数据中心的建设应在需求分析的基础上来进行分类和规划，也包括性能、冗余等方面，目前看这方面体现的不多；——在数据库总体设计中体现</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;background:yellow;\">•</span><span style=\"color:red;background:yellow;\"> </span><span style=\"font-family:宋体;color:red;background:yellow;\">云计算平台，应有更多与云技术有关的内容，比如虚拟化、分布式计算以及以服务的方式进行应用等，目前的方案这方面体现的较少，——在数据库总体设计中体现（虚拟化加些内容</span><span style=\"color:red;background:yellow;\"> </span><span style=\"font-family:宋体;color:red;background:yellow;\">云提数据立方）</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:#00B050;\">另外在数据总量上应有一个评估，一般来说</span><span style=\"color:#00B050;\">10</span><span style=\"font-family:宋体;color:#00B050;\">亿条以内，</span><span style=\"color:#00B050;\">Oracle</span><span style=\"font-family:宋体;color:#00B050;\">应该可以支撑，再多的数据量就要考虑分布式数据库，具体应根据环保局的情况来进行设计；——关系数据存储于</span><span style=\"color:#00B050;\">oracle</span><span style=\"font-family:宋体;color:#00B050;\">中，云主要存储文本数据，根据以后的数据需求再加入</span><span style=\"color:#00B050;\">Hbase</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;background:lime;\">•</span><span style=\"color:red;background:lime;\"> </span><span style=\"font-family:宋体;color:red;background:lime;\">数据表还应细化补充，例如统计分析、报表、预警、日志等，数据库涉及到的存储空间、文件、磁盘等，主键、外键、索引、关联关系等，冗余还较多；</span><span style=\"color:red;background:lime;\">-----</span><span style=\"font-family:宋体;color:red;background:lime;\">稍微体现一下</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;color:red;\">•</span><span style=\"color:red;\"> </span><span style=\"font-family:宋体;color:red;\">应用系统开发文档中验收环节应增加一些文档要求，例如用户使用报告、项目审计报告等，对维护期的工作不能限定的太死，这个阶段还有可能进行需求和代码上的修改；——应用系统开发文档</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>5</span><span style=\"font-family:宋体;\">、东南大学徐教授</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">刚才大家反应的是不同角度的看法和意见；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">数据中心建设思路应该清晰，到底走哪条路，我认为应该是提取而不是汇集，把业务系统的数据全部拿来就亏了；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">数据中心的设计应该是业务人员来做更好，一定把冗余消除掉，三步走要清晰，可以拉长项目周期；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">其他具体问题：</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<s><span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></s><s><span style=\"font-family:宋体;\">元数据系统中没有谈到；</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<s><span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></s><s><span style=\"font-family:宋体;\">还缺少一个系统设计；</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"color:red;background:lime;\">◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;color:red;background:lime;\">接口文档谈到服务，但对服务的管理没有提及；——接口规范文档</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"color:red;background:lime;\">◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;color:red;background:lime;\">系统安全运行应有版本管理；——加入对应文档</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<s><span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></s><s><span style=\"font-family:宋体;\">涉及到对元数据的增补都需要管理起来；</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;\">数据字典的设计要依靠业务人员，汲取公安系统的经验；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"color:red;background:yellow;\">◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;color:red;background:yellow;\">环保局一些有特色的元数据（噪声、污染图像、流媒体等）应有体现；——补图像</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"color:red;background:lime;\">◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;color:red;background:lime;\">应用系统开发规范中图</span><span style=\"color:red;background:lime;\">6</span><span style=\"font-family:宋体;color:red;background:lime;\">、</span><span style=\"color:red;background:lime;\">7</span><span style=\"font-family:宋体;color:red;background:lime;\">，测试用例需要单独管理，用例带有一定的继承性，新建对象不清；——图修改</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>6</span><span style=\"font-family:宋体;\">、叶书记</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">我们环保局信息化建设还比较滞后，也是在研究和摸索阶段；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">今天主要反映出三个问题</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;\">云计算平台的层次目前还没有达到，我们需要再考虑，但数据量究竟多大，目前还不明确，需要评估，要把市局、区县园区、与环保关联的其他方面，共三个层次都考虑到，来进行数据量的统计。要明确采用云计算的思路来做这件事情；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;\">数据的分类、排列和整合，都需要一线业务人员参与，目前分为两大类：环境质量和环境管理（包括污染源生命周期），下一个阶段由我亲自带队，逐个业务口和业务系统来梳理；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>◦&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style=\"font-family:宋体;\">其它具体技术性问题，请郝元和理工大学一并协调处理；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">•</span> <span style=\"font-family:宋体;\">对于一些问题，比如数据、业务的划分等等，我们内部也不是很清晰，希望借助信息化工作对内部也要有一些促进，各个业务部门要加大配合力度；</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">接下来请各方面分头工作，然后在更大范围内来讨论，争取</span><span>1</span><span style=\"font-family:宋体;\">到</span><span>2</span><span style=\"font-family:宋体;\">个月之内，我们再来检查和回顾。</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\">\r\n\t<span style=\"font-family:宋体;\">文档修改梳理：</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<span>1.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=\"font-family:宋体;\">南京环保云计算平台应用系统开发标准规范——缺王老师的图</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;background:lime;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;background:lime;\">应用系统开发规范中图</span><span style=\"color:red;background:lime;\">6</span><span style=\"font-family:宋体;color:red;background:lime;\">、</span><span style=\"color:red;background:lime;\">7</span><span style=\"font-family:宋体;color:red;background:lime;\">，测试用例需要单独管理，用例带有一定的继承性，新建对象不清；——图修改</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<s><span style=\"font-family:宋体;background:lime;\">应用系统开发文档中验收环节应增加一些文档要求，例如用户使用报告、项目审计报告等，对维护期的工作不能限定的太死，这个阶段还有可能进行需求和代码上的修改；——应用系统开发文档（项目使用报告对应用户反馈信息表）</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<s><span>2.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span></s><s><span style=\"font-family:宋体;\">接口规范文档</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;background:lime;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<s><span style=\"font-family:宋体;background:lime;\">接口文档谈到服务，但对服务的管理没有提及；——接口规范文档</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<span>3.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=\"font-family:宋体;\">数据库总体设计</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<s><span style=\"font-family:宋体;background:lime;\">数据中心的建设应在需求分析的基础上来进行分类和规划，也包括性能、冗余等方面，目前看这方面体现的不多；——在数据库总体设计中体现</span><span></span></s>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;background:yellow;\">云计算平台，应有更多与云技术有关的内容，比如虚拟化、分布式计算以及以服务的方式进行应用等，目前的方案这方面体现的较少，——在数据库总体设计中体现（虚拟化加些内容</span><span style=\"color:red;background:yellow;\"> </span><span style=\"font-family:宋体;color:red;background:yellow;\">云提数据立方）</span><span style=\"color:red;background:yellow;\">&nbsp; </span><span style=\"font-family:宋体;color:red;background:yellow;\">虚拟化</span><span style=\"color:red;background:yellow;\">VM </span><span style=\"font-family:宋体;color:red;background:yellow;\">办公</span><span style=\"font-family:宋体;color:red;\">————数据立方放在数据库总体设计中合适吗？</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;background:lime;\">数据表还应细化补充，例如<s>统计分析</s>、<s>报表、预警、日志</s>等，数据库涉及到的存储空间、文件、磁盘等，<s>主键、外键、索引、关联关系等，冗余还较多；</s></span><span style=\"background:lime;\">-----</span><span style=\"font-family:宋体;background:lime;\">稍微体现一下</span> <span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<span>4.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=\"font-family:宋体;\">分类</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;\">从信息层次的划分来看，包括</span><span style=\"color:red;\">100</span><span style=\"font-family:宋体;color:red;\">多页的数据字典，还不够清晰，主要还是基于目前工作和业务划分来做的，比如污染源应属于环境管理；辐射也涉及污染源；降尘降水属于大气等等，对信息层次的梳理需要业务人员参与；————分类重新梳理</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;\">从数据的分类上，我们公安系统划分为业务生产库、综合资源库、专题应用库三个层次，业务生产库的数据经抽取、清洗，形成综合资源库，专题应用库是对综合资源库的整合和运用。另外为了解决数据关联问题，还建立了关联库；——分类</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<span>5.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span style=\"font-family:宋体;\">数据表结构</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;background:yellow;\">环境质量相关数据表格个别地方和实际情况不一致，比如对某一次监测一般只有一个监测值，没有最大最小值；——用现有的真实入库数据</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;background:yellow;\">基础对象模块，比如污染源的基础对象</span><span style=\"background:yellow;\">(</span><span style=\"font-family:宋体;background:yellow;\">具体不太理解</span><span style=\"background:yellow;\">)</span><span style=\"font-family:宋体;background:yellow;\">——加入污染源基本信息</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;background:yellow;\">数据中心并不是对现有所有系统的所有信息进行汇集，而应是有选择的抽取和清洗；——尽量使用现有的</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span>&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;background:yellow;\">环保局一些有特色的元数据（噪声、污染图像、流媒体等）应有体现；——补图像</span><span style=\"color:red;\"></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"color:red;\">&nbsp;</span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;\">\r\n\t<span style=\"font-family:宋体;color:red;background:lime;\">数据表还应细化补充，例如统计分析、报表、预警、<s>日志</s>等，数据库涉及到的存储空间、文件、磁盘等，主键、外键、索引、关联关系等，冗余还较多；</span><span style=\"color:red;background:lime;\">-----</span><span style=\"font-family:宋体;color:red;background:lime;\">稍微体现一下</span><span></span>\r\n</p>\r\n<p class=\"MsoNormal\" style=\"margin-left:18.0pt;text-indent:-18.0pt;\">\r\n\t<span>6.<span style=\"font-size:7pt;font-family:'Times New Roman';\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><span>&nbsp;</span>\r\n</p>\r\nekekekekekekek";
            //editor_id.Value = cont;
            //网页首次加载时执行
            if (!IsPostBack)
            {
                getinfo();
           
            }
        }

        #region  删除内容
        protected void btn_delete_Click(object sender, ImageClickEventArgs e)
        {
           

        }
        #endregion

        #region  显示内容
        protected void btn_view_Click(object sender, ImageClickEventArgs e)
        {
            panel_edit.Visible = false;
            edittype = nowtype.view.ToString();
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            no= int.Parse(row.Cells[0].Text.ToString());
        
            //tb_title.Text= row.Cells[1].Text.ToString();   //  标题
            lb_thetitle.Text = row.Cells[1].Text.ToString();   //  标题
        
            string commandString =string.Format("SELECT rfcontent FROM t_knowledge where no='{0}'",no);
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                string aaaaa = ds.Tables[0].Rows[0][0].ToString();
                lb_thecontent.Text = aaaaa;
              
               // editor_id.Value=aaaaa;
               // Label1.Text = aaaaa;
            }
            panel_content_view.Visible = true;

        }
        #endregion

        #region 添加内容
        protected void btn_add_Click(object sender, ImageClickEventArgs e)
        {
            edittype = nowtype.add.ToString();
            panel_edit.Visible = true;

            getinfo();
        }
        #endregion

        #region  修改内容
        protected void btn_edit_Click(object sender, ImageClickEventArgs e)
        {
            panel_content_view.Visible = false;
            edittype = nowtype.edit.ToString();
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            no = int.Parse(row.Cells[0].Text.ToString());
            tb_title.Text = row.Cells[1].Text.ToString();   //  标题
            string commandString = string.Format("SELECT rfcontent FROM t_knowledge where no='{0}'", no);
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                string aaaaa = ds.Tables[0].Rows[0][0].ToString();
                editor_id.Value = aaaaa;
            }
            getinfo();
            panel_edit.Visible = true;
        }
        #endregion

        #region 触发gridview去获取人防工事信息
        public void getinfo()
        {
            string commandString = "SELECT no, title  FROM t_knowledge";
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

        }
        #endregion

        #region       为人防工事的gridview设置状态
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eaeaea';");//这是鼠标移到某行时改变某行的背景 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");//鼠标移走时恢复 
            }
        }
        #endregion

        #region  获取输入内容,保存到数据库
        protected void Button1_Click(object sender, EventArgs e)
        {
            panel_content_view.Visible = false;
           string content = Request.Form["editor_id"];
          //  Label1.Text = tb_title.Text;
            string title = tb_title.Text;
            if (content.Length > 0)
            {
               // this.Label1.Text = content;
                string sqlresult = string.Empty;
                if (edittype == nowtype.add.ToString())
                {
                    sqlresult = dbkit.insertandUpdate(String.Format("insert into t_knowledge(title,rfcontent)  values ('{0}','{1}')", title, content));
                    if (sqlresult.Split('@')[0] == "true")
                    {
                        dbkit.Show(this, "添加成功");
                        panel_edit.Visible = false;
                    }
                    else
                        dbkit.Show(this, "添加失败,因为" + sqlresult.Split('@')[1]);
                }
                if (edittype == nowtype.edit.ToString())
                {
                    sqlresult = dbkit.insertandUpdate(String.Format("UPDATE t_knowledge set title='{0}',rfcontent='{1}' where no='{2}'", title, content, no));
                    if (sqlresult.Split('@')[0] == "true")
                    {
                        dbkit.Show(this, "修改成功");
                        panel_edit.Visible = false;
                    }
                    else
                        dbkit.Show(this, "修改失败,因为" + sqlresult.Split('@')[1]);
                }
              
                getinfo();
            }




         
           

          
               

        }
        #endregion

        protected void btn_closeview_Click(object sender, EventArgs e)
        {
            panel_content_view.Visible = false;
            lb_thecontent.Text = "";
            lb_thetitle.Text = "";
           
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            panel_edit.Visible = false;
        }

    }
}