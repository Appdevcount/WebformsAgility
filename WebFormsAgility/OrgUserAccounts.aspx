<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgUserAccounts.aspx.cs" Inherits="WebFormsAgility.OrgUserAccounts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Content/Scripts/jquery-3.2.1.min.js"></script>
    <script src="Content/Bootstrap/js/bootstrap.min.js"></script>
    <link href="Content/Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/printStyle.css" rel="stylesheet" />
</head>
<body style="line-height: 1.4;">

    <style type="text/css">
        /*div .row {
            border-width: 5px;
            border-radius: 1px;
            border: 1px solid;
        }*/

        /*div .catg {
            border-width: 5px;
            border-radius: 1px;
            border: 1px solid;
        }

        .PadArr {
            margin-top:5px;
            margin-bottom:2px;
        }*/

        body {
            -webkit-print-color-adjust: exact;
        }

        .title1 {
            font-size: 18px;
            font-weight: bold;
        }

        .title2 {
            font-size: 16px;
            font-weight: bold;
        }

        .header1 {
            font-size: 14px;
            font-weight: bold;
            color: white;
            background-color: silver !important;
            text-align: center;
        }

        .formlabel {
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }

        .chkboxtd {
            width: 12px;
            text-align: center;
            border-left: none;
        }

        .chkboxlbltd {
            width: 18%;
            text-align: center;
            border-right: none;
            margin-right: 5px;
            margin-left: 2px;
        }

        .BigCheckBox input /*To increase the ASP.NET checkbox input element size*/
        {width:18px; height:18px;}
       
    </style>
    <form id="form1" runat="server">
        <%
            int pageSize = 7;
            int pageCount = (int)Math.Ceiling((decimal)((dtUsers.Rows.Count - 1) / pageSize)) + 1;
            ;

            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                int istart = (pageNumber - 1) * pageSize;
                int iend = Math.Min(pageNumber * pageSize, dtUsers.Rows.Count);
            %>

        <div class="container small">
            <div class="row">
                <div class="col-10 font-weight-bold small text-center title1">
                    <div style="margin-left: 79px;">
                        الإدارة العامة للجمارك<br />
                        General Administration of Customs<br />
                        مشروع تطوير وتشغيل وصيانة الخدمات المساندة للعمل الجمركي 
                    </div>
                </div>
                <div class="col-2 PadArr">
                    <img style="height: 50px; width: 50px;" src="Content/Images/Kuwait%20Customs.png" />
                </div>
            </div>
            <div class="row catgTopB title2">
                <div class="col-6 text-center" style="margin-top:6px;">Organizations User Accounts Delivery Form</div>
                <div class="col-6 text-center" style="margin-top:6px;">نموذج إستلام حسابات مستخدمي الشركات</div>
            </div>
            <div class="row catg font-weight-bold" style="direction: rtl; border: none; margin-top: 10px; margin-bottom: 10px">
                <table border="1" style="border: 1px solid; border-collapse: collapse; width: 100%; margin-bottom: 4px;">
                    <tr>
                        
                        <td class="chkboxtd" style="width:4%" >
                            <asp:CheckBox ID="CheckBox4" CssClass="BigCheckBox" runat="server" />
                        </td>
                        <td class="chkboxlbltd" style="padding-top: 4px;padding-bottom: 4px;width:16%">وكيل شحن بحري
                        <br />
                            Sea Carrier and shipping Agent
                        </td>
                        <td class="chkboxtd" style="width:4%">
                            <asp:CheckBox ID="CheckBox3" CssClass="BigCheckBox" runat="server" />
                        </td>
                        <td class="chkboxlbltd" style="width:16%">وكيل تجزئة / وسيط شحن جوي 
                        <br />
                            Air Freight Forwarder
                        </td>

                        <td class="chkboxtd" style="width:4%">
                            <asp:CheckBox ID="CheckBox2" CssClass="BigCheckBox" runat="server" />
                        </td>
                        <td class="chkboxlbltd" style="width:16%">وكيل خدمات أرضية جوي
                        <br />
                            Air Ground Handler
                        </td>

                        <td class="chkboxtd" style="width:4%">
                            <asp:CheckBox ID="CheckBox1" CssClass="BigCheckBox" runat="server" />
                        </td>
                        <td class="chkboxlbltd" style="width:16%">وكلاء بريد سريع بري
                        <br />
                            Land Postal Express Courier
                        </td>

                        <td class="chkboxtd" style="width:4%">
                            <asp:CheckBox ID="CheckBox0" CssClass="BigCheckBox" runat="server" />
                        </td>
                        <td class="chkboxlbltd" style="width:16%">مستثمر مستودعات جمركية عامة/خاصة
                            <br />
                            BWH operator
                        </td>
                    </tr>


                </table>
            </div>
            <div class="row catg font-weight-bold" style="direction: rtl; border: none; margin-bottom: 2px;">
                <table style="direction: rtl; border: 1px solid; border-collapse: collapse; width: 100%; margin-left: 0px;">
                    <tr>
                        <td colspan="2" class="header1" style="width: 50%">مـــــعـــلـــومـــات الــشــركــة</td>
                        <td colspan="2" class="header1" style="width: 50%">Organization Information</td>
                    </tr>
                    <tr>
                        <td class="formlabel"  style="padding-top: 4px;padding-bottom: 4px;">اسم الشركة باللغة العربية<br />
                            Organization Name in Arabic
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="OrgName" runat="server" Width="466px" ReadOnly="True" CssClass="text-center"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="formlabel"  style="padding-top: 4px;padding-bottom: 4px;">كود المنظمة بالنظام<br />
                            Organization Code
                        </td>

                        <td>
                            <asp:TextBox ID="OrgCd" Width="150px" ReadOnly="True" CssClass="text-center" runat="server"></asp:TextBox>
                        </td>
                        <td class="formlabel">رقم السجل التجاري<br />
                            Trade License No.
                        </td>
                        <td>
                            <asp:TextBox ID="TrdLicNo" Width="150px" ReadOnly="True" CssClass="text-center" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

           
            <div class="row catg font-weight-bold" style="border: none; margin-bottom: 8px;">
                <table style="border: 1px solid; border-collapse: collapse; width: 100%; padding: 0px 0px 0px 0px;">
                    <tr>
                        <td colspan="2" class="header1" style="width: 50%">User Actions Information</td>
                        <td colspan="2" class="header1" style="width: 50%">مـــــعـــلـــومـــات إجراءات الــمــســتــخــدم</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="align-items: center; align-content: center; text-align: center;">
                            <div style="align-items: center; align-content: center; text-align: center; margin-bottom: 4px;margin-top: 4px; min-height:320px">

                            <table border="1" style="border: 1px solid black; border-collapse: collapse; width: 70%; margin-left: auto; margin-right: auto; padding: 0px 0px 0px 0px;">
                               
                                        <thead>
                                            <tr>
                                                <th class="text-center">Accounts Details<br />
                                                    بيانات الحساب</th>
                                                <th style="width: 11%;" class="text-center">Update<br />
                                                    تحديث</th>
                                                <th style="width: 11%;" class="text-center">New<br />
                                                    جديد</th>
                                                <th class="text-center" style="width: 11%;">No.<br />
                                                    م</th>
                                            </tr>
                                        </thead>                                    
                                    <%  
                                        for (int i = istart; i < iend; i++)
                                        {
                                            System.Data.DataRow drUser = dtUsers.Rows[i];

                                    %>
                 

 <tr>
                                            <td style="text-align: left; border-bottom: none;">
                                                <div style="width: 40px; display: inline-block; margin-left: 7px">Name</div>
                                                :&nbsp;<%= drUser["UserName"].ToString()%> </td>
                                            <td rowspan="2">
                                                <div class="text-center" style="margin-top: 14px;    margin-left: 21px;">
                                                    
                                                    <input type="checkbox" class="form-check text-center TbleChecboxes" style="margin-top: 14px;" id="Update" <% if (drUser["Updated"].ToString() == "True")
                                                        { %> checked="" <%}   %>/>

                                                    <%--<input type="checkbox" class="form-check text-center TbleChecboxes" id="Update" />--%>
                                                    <%--checked="'<%# Eval("Updated").ToString() == "True" %>'" --%>
                                                </div>
                                            </td>
                                            <td rowspan="2">
                                                <div class="text-center" style="margin-top: 14px;    margin-left: 21px;">
                                                    <input type="checkbox" class="form-check text-center TbleChecboxes" id="NewCHK" <% if (drUser["New"].ToString() == "True")
                                                        { %> checked="" <%}   %>/>
                                                </div>
                                            </td>
                                            <td rowspan="2" class="text-center">
                                                <div style="margin-top: 5px;"><%= (i+1) %></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; border-top: none;">
                                                <div style="width: 40px; display: inline-block; margin-left: 7px">UserId</div>
                                                :&nbsp;<%= drUser["UserId"].ToString()%> </td>
                                        </tr>



            <%
                }


                %>
                                                                          
                            </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="row text-right catg" style="margin-bottom: 7px;">
                <div class="col-12" style="direction: rtl; margin-top: 7px; margin-bottom: 7px">
                    <asp:CheckBox ID="CheckBox5" CssClass="" runat="server" />
                    <b>أنا الموقع أدناه وبصفتى مفوض بالتوقيع </b>أتحمل المسئولية الكاملة عن كافة المستخدمين المذكورين أعلاه فىما يخص الالتزام بقانون الجمارك الموحد  ولائحته التنفيذية وما يصدر عن الإدارة العامة للجمارك  من قرارات وتعليمات بما فيها استخدام الخدمات الالكترونية بالنظام الجمركي الآلي وقد إستلمت رمز مستخدم و أتعهد ببذل كل السبل المتاحة من أجل المحافظة على سلامة الرموز وكلمه السر وعدم منح رموز الاستخدام للغير و طلب إلغاء رمز الاستخدام في حالة تغير صفة المستخدم لأي سبب كان أو انتهاء علاقته بالشركة وإبلاغ الإدارة العامة للجمارك فوراً في حال فقدان رمز الاستخدام أو كلمه السر لأي من المستخدمين التابعين للشركة  وأقر بصلاحية بيانات المستخدمين المقدمة طوال مدة الإستخدام للنظام  وأن أقوم بتحديث أي بيانات فور تعديلها لابقائها حقيقية ودقيقة وحديثة وكاملة.

                </div>
                <%--<div class="col-12 catgAdjust">(<b>الأولية</b>) حسب سياسات أمن المعلومات المتبعه ، ترسل كلمة السر  
                    بشكل تلقائي من النظام الجمركى الآلي إلى كل مستخدم على بريده الإلكتروني الخاص المسجل بالملف الشخصى للمستخدمين الجدد**</div>--%>
                <div class="col-12 catgAdjust">
                    <div style="margin-top: 8px; margin-bottom: 8px; direction: rtl">
                        <table>
                            <tr>
                                <td style="width: 3%">**</td>
                                <td style="width: 97%">.حسب سياسات أمن المعلومات المتبعه ، ترسل كلمة السر   (<b>الأولية</b>) 
                   بشكل تلقائي من النظام الجمركى الآلي إلى كل مستخدم على بريده الإلكتروني الخاص المسجل بالملف الشخصى للمستخدمين الجدد </td>

                            </tr>
                        </table>

                    </div>
                </div>
                <div class="col-12 catgAdjust" style="margin-top: 8px; margin-bottom: 8px">
                    <table style="border: none; border-collapse: collapse; width: 100%; direction: rtl; padding: 0px 0px 0px 0px;">
                        <tr>
                            <td style="padding-bottom: 30px;padding-top: 4px;">إسم المفوض بالتوقيع</td>
                            <td style="padding-bottom: 30px;">:______________________________________</td>
                            <td style="padding-bottom: 30px;padding-top: 4px;">توقيع المفوض بالتوقيع</td>
                            <td style="padding-bottom: 30px;">:______________________________________</td>
                        </tr>
                        <tr>
                            <td>المسمى الوظيفي</td>
                            <td>:______________________________________</td>
                            <td>التاريخ</td>
                            <td>:______________________________________</td>
                        </tr>

                    </table>
                </div>
            </div>
            <div class="row small catg" style="margin-top: 8px;"">
                <div style="margin-left: 5px; margin-top: 6px; margin-bottom: 6px">
                    For additional information please contact Support Inquiry <b><u>cs.support@customs.gov.kw</u></b> or call <b>24981234.</b>
                </div>
            </div>
        </div>
        <div class="container small" style="margin-top: 10px;">
            <footer>
                <div class="row catg small text-sm-center">
                    <div class=" col-3 catg" style="text-align: left">
                        <b>Document Name</b>
                        <br />
                        Request Form for User Registration
                    </div>
                    <div class="col-3 catg">
                        <b>Document Number</b>
                        <br />
                        ITCS-RF-004
                    </div>
                    <div class="col-2 catg">
                        <b>Issued</b>
                        <br />
                        01.10.2017
                    </div>
                    <div class="col-2 catg">
                        <b>Effective</b>
                        <br />
                        01.10.2017
                    </div>
                    <div class="col-1 catg">
                        <b>Revision</b>
                        <br />
                        1
                    </div>
                    <div class="col-1 catg">Page
                        <br />
                        <b><%=pageNumber %></b> of <b><%=pageCount %></b></div>
                </div>

            </footer>
        </div>
        <% if (pageNumber < pageCount)
            {%><p style="page-break-after:always"/> <%} %>
         <%
             }
            %>
    </form>
    <script type="text/javascript">
        $(function () {

            <%--$("#<%=UserDetailsRptr.FindControl("NewCHK").ClientID%>,#<%=UserDetailsRptr.FindControl("UpdateCHK").ClientID%>")--%>
            //$('[id *= UserDetailsRptr_NewCHK]')
            //    //.css({
            //    //    'margin-left': '37px',
            //    //    'margin-top': '14px',
            //    //});
            //    .addClass("TbleChecboxes");
            //$('[id *= UserDetailsRptr_UpdateCHK]')
            //    //.css({
            //    //    'margin-left': '37px',
            //    //    'margin-top': '14px',
            //    //});
            //    .addClass("TbleChecboxes");
        })
    </script>
</body>
</html>
