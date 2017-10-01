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
    </style>
    <form id="form1" runat="server">

        <div class="container small">
            <div class="row catg">
                <div class="col-10 font-weight-bold small text-center ">
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
            <div class="row catg">

                <div class="col-6 text-center">Organizations User Accounts Delivery Form</div>
                <div class="col-6 text-center">نموذج إستلام حسابات مستخدمي الشركات</div>


            </div>
            <div class="row catg">
                <div class="col-1" style="padding-right: 0px;"></div>
                <div class=" col-2 catg">

                    <span style="float: right; position: relative; margin-top: 16px;">
                        <asp:CheckBox ID="CheckBox0" CssClass="custom-checkbox" runat="server" />
                    </span>

                    مستثمر مستودعات جمركية عامة/خاصة
 BWH operator

                </div>
                <div class="col-2 catg">
                    <span style="float: right; position: relative; margin-top: 16px;">
                        <asp:CheckBox ID="CheckBox1" CssClass="custom-checkbox" runat="server" />
                    </span>
                    <span>وكلاء بريد سريع بري
                        <br />
                        Land Postal Express Courier
                    </span>


                </div>
                <div class="col-2 catg">
                    <span style="float: right; position: relative; margin-top: 16px;">
                        <asp:CheckBox ID="CheckBox2" CssClass="custom-checkbox" runat="server" /></span>
                    <span>وكيل خدمات أرضية جوي
                        <br />
                        Air Ground Handler  </span>



                </div>
                <div class="col-2 catg">

                    <span style="float: right; position: relative; margin-top: 16px;">
                        <asp:CheckBox ID="CheckBox3" CssClass="custom-checkbox" runat="server" />
                    </span>

                    <span>وكيل تجزئة / وسيط شحن جوي 
                        <br />
                        Air Freight Forwarder</span>




                </div>
                <div class="col-2 catg">

                    <span style="float: right; position: relative; margin-top: 16px;">
                        <asp:CheckBox ID="CheckBox4" CssClass="custom-checkbox" runat="server" /></span>

                    <span>وكيل شحن بحري
                        <br />
                        Sea Carrier and shipping Agent</span>


                </div>
                <div class="col-1"></div>
            </div>

            <div class="row text-center catg">
                <div class="col-6">Organization Information</div>
                <div class="col-6">مـــــعـــلـــومـــات الــشــركــة </div>
            </div>
            <div class="row catg">
                <div class="col-8" style="margin-top: 5px;">
                    <asp:TextBox ID="OrgName" runat="server" Width="450px" ReadOnly="True" CssClass="text-center"></asp:TextBox>
                </div>

                <div class="col-4 text-center">
                    اسم الشركة باللغة العربية<br />
                    Organization Name in Arabic
                </div>
            </div>
            <div class="row catg">
                <div class="col-6">
                    <div class="row">
                        <div class="col-6" style="margin-top: 5px">
                            <asp:TextBox ID="OrgCd" Width="150px" ReadOnly="True" CssClass="text-center" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-6 text-center">
                            كود المنظمة بالنظام<br />
                            Organization Code
                        </div>
                    </div>
                </div>
                <div class="col-6">
                    <div class="row">
                        <div class="col-6" style="margin-top: 5px">
                            <asp:TextBox ID="TrdLicNo" Width="150px" ReadOnly="True" CssClass="text-center" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-6 text-center">
                            رقم السجل التجاري<br />
                            Trade License No.
                        </div>
                    </div>
                </div>
            </div>

            <div class="row text-center catg ">
                <div class="col-6">User Actions Information</div>
                <div class="col-6">مـــعـــلــومـــات إجراءات الــمــســتــخــدم </div>
            </div>

            <br />

            <style type="text/css">
                /*tbody > tr > td {
                    height: 20px;
                    padding: 0px;
                    border-top: 0px;
                }*/
            </style>

            <%--            <div class="row ">
         
                <table class="table table-bordered table-hover table-sm">
                    <thead>
                        <tr>
                            <th>Accounts Details بيانات الحساب</th>
                            <th>Update
تحديث
                            </th>
                            <th>New
جديد
                            </th>
                            <th>م</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                        <tr>
                            <td><small>Name:</small></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                            <td rowspan="2"></td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small></td>
                        </tr>
                    </tbody>
                </table>
            </div>--%>
            <div class="row">

                <asp:Repeater ID="UserDetailsRptr" runat="server">
                    <HeaderTemplate>

                        <table class="table table-bordered table-hover table-active table-sm ">
                            <thead>
                                <tr>
                                    <th class="text-center">Accounts Details بيانات الحساب</th>
                                    <th style="width: 11%;" class="text-center">Update
تحديث
                                    </th>
                                    <th style="width: 11%;" class="text-center">New
جديد
                                    </th>
                                    <th class="text-center" style="width: 11%;">No. م</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>

                        <tr>
                            <td><small>Name:</small> <%#Eval("UserName") %> </td>
                            <td rowspan="2">
                                <div style="margin-top: 14px;">
                                    <asp:CheckBox ID="UpdateCHK" CssClass="form-check text-center " runat="server" Checked='<%# Eval("Updated").ToString() == "True" %>' />

                                    <%--<input type="checkbox" class="form-check text-center TbleChecboxes" id="Update" />--%>
                                    <%--checked="'<%# Eval("Updated").ToString() == "True" %>'" --%>
                                </div>
                            </td>
                            <td rowspan="2">
                                <div style="margin-top: 14px;">
                                    <asp:CheckBox ID="NewCHK" CssClass="form-check text-center " runat="server" Checked='<%# Eval("New").ToString() == "True" %>' />
                                    <%--<input type="checkbox" class="form-check text-center TbleChecboxes" id="New" />--%>
                                    <%--checked="'<%# Eval("New").ToString() == "True" %>'" --%>
                                </div>
                            </td>
                            <td rowspan="2" class="text-center">
                                <div style="margin-top: 14px;"><%# Container.ItemIndex + 1 %></div>
                            </td>
                        </tr>
                        <tr>
                            <td><small>UserId:</small> <%#Eval("UserId") %> </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="row text-right catg">
                <div class="col-12">
                    <b>أنا الموقع أدناه وبصفتى مفوض بالتوقيع </b>أتحمل المسئولية الكاملة عن كافة المستخدمين المذكورين أعلاه فىما يخص الالتزام بقانون الجمارك الموحد  ولائحته التنفيذية وما يصدر عن الإدارة العامة للجمارك  من قرارات وتعليمات بما فيها استخدام الخدمات الالكترونية بالنظام الجمركي الآلي وقد إستلمت رمز مستخدم و أتعهد ببذل كل السبل المتاحة من أجل المحافظة على سلامة الرموز وكلمه السر وعدم منح رموز الاستخدام للغير و طلب إلغاء رمز الاستخدام في حالة تغير صفة المستخدم لأي سبب كان أو انتهاء علاقته بالشركة وإبلاغ الإدارة العامة للجمارك فوراً في حال فقدان رمز الاستخدام أو كلمه السر لأي من المستخدمين التابعين للشركة  وأقر بصلاحية بيانات المستخدمين المقدمة طوال مدة الإستخدام للنظام  وأن أقوم بتحديث أي بيانات فور تعديلها لابقائها حقيقية ودقيقة وحديثة وكاملة

                </div>
                <div class="col-12 catgAdjust">حسب سياسات أمن المعلومات المتبعه ، ترسل كلمة السر (<b> الأولية</b>) بشكل تلقائي من النظام الجمركى الآلي إلى كل مستخدم على بريده الإلكتروني الخاص المسجل بالملف الشخصى للمستخدمين الجدد **</div>
                <div class="col-12 catgAdjust">
                    ____________________________ :توقيع المفوض بالتوقيع:____________________________        إسم المفوض بالتوقيع    
                <br />

                    _____________________________________:المسمى الوظيفي: ______________________________________     التاريخ                                                 

            
                </div>
            </div>
            <%--    <div class="row text-right catg"></div>--%>
            <%--<div class="row text-right catg">--%>

            <div class="row small catg">For additional information please contact Support Inquiry cs.support@customs.gov.kw or call 24981234. </div>


        </div>
        <br />
        <div class="container small">
            <footer>
                <div class="row catg small text-sm-center">
                    <div class=" col-3 catg">
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
                        <b>Issued:</b>
                        <br />
                        01.10.2017
                    </div>
                    <div class="col-2 catg">
                        <b>Effective:</b>
                        <br />
                        01.10.2017
                    </div>
                    <div class="col-1 catg">
                        <b>Revision:</b>
                        <br />
                        1
                    </div>
                    <div class="col-1 catg">Page <b>1</b> of <b>1</b></div>
                </div>

            </footer>
        </div>
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
