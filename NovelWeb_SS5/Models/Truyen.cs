﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NovelWeb_SS5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Truyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Truyen()
        {
            this.Chuongs = new HashSet<Chuong>();
        }

        public int idTruyen { get; set; }
        public Nullable<int> idTL { get; set; }
        [DisplayName("Tên Truyện")]
        public string tenTruyen { get; set; }
        [DisplayName("Tác Giả")]
        public string tacGia { get; set; }
        [DisplayName("Tình Trạng")]
        public string tinhTrang { get; set; }
        [DisplayName("Ảnh Đại Diện")]
        public string anhDaidien { get; set; }
        [DisplayName("Tóm Tắt")]
        public string tomTat { get; set; }
        [DisplayName("Ngày Đăng")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ngayDang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chuong> Chuongs { get; set; }
        [DisplayName("Thể Loại")]
        public virtual TheLoai TheLoai { get; set; }
    }
}
