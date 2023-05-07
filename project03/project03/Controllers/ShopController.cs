using project03.Models.entiy;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace project03.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        Model1 model = new Model1();
        public void display_ca()
        {
            if(Session["id_account"]==null)
            {
                Session["sum_item_car"] = 0;
            }
            else
            {
                int s = 0;
                int account = int.Parse(Session["id_account"].ToString());
                var sum = model.dathang.Where(c => c.id_account == account).ToList();
                foreach (var item in sum)
                {
                    s = s + item.quantity;
                }

                Session["sum_item_car"] = s;


            }
          
        }
        public ActionResult Index()
        {
            display_ca();
            return View();
        }


        public ActionResult Index1()
        {
            return PartialView("Main");
        }

        public ActionResult render_product()
        {
           // !String.IsNullOrEmpty(Request.QueryString["pID"]
            
            decimal sum = 0;
            List<HangHoa> hanghoa;
            var result = model.HangHoas.ToList();
            foreach (var item in result)
            {
                sum = sum + 1;
            }
            ViewBag.sum = Math.Ceiling(sum / 5);
            if (!String.IsNullOrEmpty(Request.QueryString["page"]))
            {
                int page = int.Parse(Request.QueryString["page"].ToString());
                hanghoa = model.HangHoas.OrderBy(s => s.MaHang).Skip((page - 1) * 5).Take(5).ToList();
                return PartialView("New_product", hanghoa);
            }
             else
            {
                hanghoa = model.HangHoas.OrderBy(s => s.MaHang).Skip(0).Take(5).ToList();
                //   hanghoa = model.HangHoas.Where(s=>s.Gia>=1000).ToList();
                return PartialView("New_product", hanghoa);

            }
          
           // return Redirect("/shop/render_product", hanghoa);
        }

     /*   public ActionResult render_product1(int page)
        {

            var hanghoa = model.HangHoas.OrderBy(s => s.MaHang).Skip((page - 1) * 5).Take(5).ToList();
            return PartialView("New_product", hanghoa);
        }*/

        public ActionResult render(int Id)
        {
            //Model1 model1 = new Model1();
            var hanghoa= model.HangHoas.Where(h => h.MaLoai == Id).ToList();
            return PartialView("New_product", hanghoa);
        }

        public ActionResult render_feature()
        {
            Model1 model1 = new Model1();
            var hanghoa = model1.HangHoas.Where(h => h.Gia >= 1200).ToList();
            return PartialView("Feature_Main", hanghoa);
        }
        [HttpGet]
        public ActionResult uploadanh()
        {
            return View();
        }

       
        public ActionResult search(string Item)
        {
            /*var hanghoa = (from h in model.HangHoas
                          where h.TenHang.Contains(Item)
                          select h).ToList() ;*/
            var hanghoa = model.HangHoas.Where(h => h.TenHang.Contains(Item)).ToList();
            return PartialView("New_product", hanghoa);

            //c2
            //  var listhang = model.HangHoas.Where(h => h.TenHang.Contains(item)).ToList();

         
        }


        public ActionResult cart(int item)
        {
            if(Session["id_account"]==null)
            {
                return RedirectToAction("index", "login");
            }
            else
            {
                var id_account =int.Parse(Session["id_account"].ToString());
                var order = model.dathang.FirstOrDefault(s=>s.id_account== id_account && s.id_sanpham==item);
                if (order == null)
                {
                    var pro = model.HangHoas.FirstOrDefault(s => s.MaHang == item);
                    dathang dat = new dathang();
                    dat.id_account = int.Parse(Session["id_account"].ToString());
                    dat.id_sanpham = pro.MaHang;
                    dat.quantity = 1;
                    model.dathang.Add(dat);
                    model.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    order.quantity = order.quantity + 1;
                    model.SaveChanges();

                    return RedirectToAction("index", "shop");
                }    
            }
   
        }


        public ActionResult show_card()
        {

            if (Session["id_account"] == null)
            {
                // var query = new dathang();
                return RedirectToAction("index", "shop");
            }
            else
            {
                int account = int.Parse(Session["id_account"].ToString());
                var query = (from o in model.dathang
                             join c in model.HangHoas on o.id_sanpham equals c.MaHang
                             where o.id_account == account
                             select new two
                             {
                                 id_hang = o.id_hang,
                                 id_sp = o.id_sanpham,
                                 gia = c.Gia,
                                 name = c.TenHang,
                                 anh = c.Anh,
                                 soluong = o.quantity
                             }).ToList();


                return View(query);
            }
  
        }

        public ActionResult update_soluong(dathang dathang)
        {
           
            var objCourse = model.dathang.Single(course => course.id_hang == dathang.id_hang);
            //Field which will be update  
            objCourse.quantity = dathang.quantity;
            // executes the appropriate commands to implement the changes to the database

            model.SaveChanges();
            display_ca();

            return RedirectToAction("show_card");
        }


        public ActionResult delete_item_cart(dathang dathang)
        {
            
            var item=model.dathang.Find(dathang.id_hang);
            //
            model.dathang.Remove(item);
            // executes the appropriate commands to implement the changes to the database
            model.SaveChanges();
            display_ca();
            return RedirectToAction("show_card");
        }



        [HttpPost]
        public ActionResult uploadanh(HttpPostedFileBase h)
        {
            if (h.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(h.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/images"), _FileName);
                h.SaveAs(_path);
            }
            return View();
        }


        public ActionResult thanhtoan()
        {
            if (Session["email"] == null && Session["diachi"] == null && Session["sodienthoai"] == null)
            {
                return RedirectToAction("khach_hang_thong_tin","shop");
            }
                else
            {
                float sum_tienthanhtoan = 0;
                var noidung = "";
                //   int account = int.Parse(Session["id_account"].ToString());
                int account = int.Parse(Session["id_account"].ToString());
                var a = model.dathang.Where(s => s.id_account == account).ToList();
                // tinh tien can thanh toan
                var query = (from o in model.dathang
                             join c in model.HangHoas on o.id_sanpham equals c.MaHang
                             where o.id_account == account
                             select new two
                             {
                                 id_hang = o.id_hang,
                                 id_sp = o.id_sanpham,
                                 gia = c.Gia,
                                 name = c.TenHang,
                                 anh = c.Anh,
                                 soluong = o.quantity
                             }).ToList();

                foreach (var o in query)
                {
                    sum_tienthanhtoan = sum_tienthanhtoan + (o.soluong * int.Parse(o.gia.ToString()));
                }
                // end tinh tien thanh toan
                foreach (var item in a)
                {
                    //noi dung gui ve mail khachhang
                    var tenhang = model.HangHoas.FirstOrDefault(s => s.MaHang == item.id_sanpham);
                    noidung = noidung + (tenhang.TenHang).ToString() + " số lượng " + (item.quantity).ToString() + "<br>";
                    //end noi dung gui ve mail khachhang

                    //them du lieu vao lich su mua hang
                    lichsumuahang lichsu = new lichsumuahang();
                    lichsu.id_sanpham = item.id_sanpham;
                    lichsu.quantity = item.quantity;
                    lichsu.id_account = item.id_account;
                    lichsu.thoigianmua = DateTime.Now;
                    lichsu.sodienthoai = Session["sodienthoai"].ToString();
                    lichsu.email = Session["email"].ToString();
                    lichsu.diachi = Session["diachi"].ToString();
                    model.lichsumuahang.Add(lichsu);
                    //end them du lieu vao lich su mua hang
                    //xoa san pham khoi gio hang sau khi thanh toan
                    var b = model.dathang.Find(item.id_hang);
                    model.dathang.Remove(b);
                    //end xoa san pham khoi gio hang sau khi thanh toan
                }
                string sendMail = "";
                if (noidung != "")
                {
                    noidung = noidung + "tổng tiền hàng: " + sum_tienthanhtoan
                    + "<br>" + "phí vận chuyển: free ship" + "<br>" + "vat: 10%" + "<br>" + "tổng tiền thanh toán: " + (sum_tienthanhtoan + (sum_tienthanhtoan * (10.0 / 100.0)));
                    try
                    {
                        string email_nguoinhan = Session["email"].ToString();
                        string fromEmail = "nguyenvanthang2k000123@gmail.com";
                        MailMessage mailMessage = new MailMessage(fromEmail, email_nguoinhan, "thông báo đặt hàng thành công", noidung);
                        mailMessage.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("nguyenvanthang2k000123@gmail.com", "hzvnthbdjdxushew");
                        smtpClient.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        sendMail = ex.Message.ToString();
                        Console.WriteLine(ex.ToString());
                    }
                }

                model.SaveChanges();
                /* return RedirectToAction("index", "shop");*/
                return View("sucess");

            }
        }



        public ActionResult khach_hang_thong_tin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult khach_hang_thong_tin(thongtin info)
        {
            Session["email"] = info.email;
            Session["diachi"] = info.diachi;
            Session["sodienthoai"] = info.sodienthoai;
            return RedirectToAction("show_card","shop");
        }


        public ActionResult Detail_product(int id)
        {
            preview pr = new preview();
            var a =  model.HangHoas.FirstOrDefault(s => s.MaHang == id);
            ViewBag.user = model.tblAccounts.ToList();
            var b = model.post.Where(s => s.id_sanpham == id).ToList();
            pr.hanghoa = a;
            pr.post = b;
            return View(pr);
        }

        [HttpPost]
        public ActionResult post_comment(post post)
        {
            if (Session["id_account"] == null)
            {
                // var query = new dathang();
                return RedirectToAction("index", "Login");
            }
            else
            {
                 int account = int.Parse(Session["id_account"].ToString());
                //tim xem khach hang da tung mua hang chua , neu mua roi duoc binh luan
                 var account_id = model.lichsumuahang.FirstOrDefault(s => s.id_account == account && s.id_sanpham==post.id_sanpham);

                if (account_id != null)
                {
                    post p = new post();
                    p.id_sanpham = post.id_sanpham;
                    p.content_post = post.content_post;
                    p.id_account = account;
                    p.ngay_post = DateTime.Now;
                    p.danhgia = post.danhgia;

                    model.post.Add(p);
                    model.SaveChanges();
                    return Redirect("/shop/Detail_product?id="+post.id_sanpham);
                }
                else
                {
                    TempData["err_comment"] = "lưu ý: chỉ có khách hàng đã mua sản phẩm mới có quyền đánh giá sản phẩm";
                    return Redirect("/shop/Detail_product?id=" + post.id_sanpham);
                }
            }

        }

    }
        

              


    }
