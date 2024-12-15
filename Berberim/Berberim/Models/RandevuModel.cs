﻿using System.ComponentModel.DataAnnotations;

namespace Berberim.Models
{
		public class RandevuModel
		{


			[Required(ErrorMessage = "Ad Soyad alanı gereklidir.")]
			[StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olmalıdır.")]
			public string adSoyad { get; set; }

			// Hizmet Seçimi
			[Required(ErrorMessage = "Hizmet seçiniz.")]
			public string service { get; set; }

			[Required(ErrorMessage = "Personel seçiniz.")]
			public string personnel { get; set; }

			// Tarih
			[Required(ErrorMessage = "Tarih alanı gereklidir.")]
			[DataType(DataType.Date)]
			public DateTime date { get; set; }

			// Saat
			[Required(ErrorMessage = "Saat alanı gereklidir.")]
			[DataType(DataType.Time)]
			public TimeSpan saat { get; set; } // string kullanıyoruz çünkü saat time olarak inputta alınıyor.

			// Telefon Numarası
			[Required(ErrorMessage = "Telefon numarası gereklidir.")]
			[Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
			//     [StringLength(15, ErrorMessage = "Telefon numarası en fazla 15 karakter olabilir.")]
			public string PhoneNumber { get; set; }

			// Notlar
			//[StringLength(500, ErrorMessage = "Notlar en fazla 500 karakter olabilir.")]
			public string Notes { get; set; }

		}

	}

