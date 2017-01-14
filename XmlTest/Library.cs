using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace XmlTest
{
    /// <summary>
    /// 分類コード
    /// </summary>
    public enum CCode
    {
        /// <summary>
        /// 日本文学、小説・物語
        /// </summary>
        [XmlEnum("C0193")]
        C0193 = 1,

        /// <summary>
        /// 電子通信
        /// </summary>
        [XmlEnum("C3055")]
        C3055
    }

    /// <summary>
    /// 蔵書
    /// </summary>
    [XmlRoot("Library")]
    public class Library
    {
        /// <summary>
        /// 本
        /// </summary>
        [XmlArray("Books")]
        [XmlArrayItem("Book")]
        [Required, ValidateObject]
        public List<Book> Books { get; set; }
    }

    /// <summary>
    /// 本
    /// </summary>
    [Serializable]
    public class Book
    {
        /// <summary>
        /// ISBN
        /// </summary>
        [XmlAttribute("ISBN")]
        [Required]
        public string ISBN { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        [XmlIgnore]
        public int? Price { get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        [XmlElement("Price"), Display(Name = "Price")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Required]
        public string PriceSpecified
        {
            get
            {
                return Price.HasValue ? Price.ToString() : null;
            }

            set
            {
                Price = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?);
            }
        }

        /// <summary>
        /// タイトル
        /// </summary>
        [XmlElement("Title")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 分類コード
        /// </summary>
        [XmlIgnore]
        public CCode? CCode { get; set; }

        /// <summary>
        /// 分類コード
        /// </summary>
        [XmlElement("CCode"), Display(Name = "CCode")]
        [Required, EnumDataType(typeof(CCode))]
        public string CCodeSpecified
        {
            get
            {
                return CCode.HasValue ? CCode.ToString() : null;
            }

            set
            {
                CCode = Enum.IsDefined(typeof(CCode), value) ? (CCode)Enum.Parse(typeof(CCode), value) : default(CCode);
            }
        }
    }
}
