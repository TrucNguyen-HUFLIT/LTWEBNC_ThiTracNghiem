USE [TracNghiemOnline]
GO
/****** Object:  Table [dbo].[BaiThi]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaiThi](
	[IDBaiThi] [nvarchar](50) NOT NULL,
	[TenBaiThi] [nvarchar](50) NULL,
	[ThoiGianLamBai] [time](7) NULL,
 CONSTRAINT [PK_BaiThi_1] PRIMARY KEY CLUSTERED 
(
	[IDBaiThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaiThiHS]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaiThiHS](
	[IDBaiThiHS] [nvarchar](50) NOT NULL,
	[IDBaiThi] [nvarchar](50) NOT NULL,
	[ThoiGianLamBai] [time](7) NULL,
	[TongSoDiem] [varchar](50) NULL,
	[IDStudent] [nvarchar](50) NULL,
 CONSTRAINT [PK_BaiThiHS] PRIMARY KEY CLUSTERED 
(
	[IDBaiThiHS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauHoi]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauHoi](
	[IDCauHoi] [nvarchar](50) NOT NULL,
	[CauHoi] [nvarchar](50) NULL,
	[A] [nvarchar](50) NULL,
	[B] [nvarchar](50) NULL,
	[C] [nvarchar](50) NULL,
	[D] [nvarchar](50) NULL,
	[DapAn] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CauHoi] PRIMARY KEY CLUSTERED 
(
	[IDCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTBT]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTBT](
	[IDBaiThi] [nvarchar](50) NOT NULL,
	[IDCauHoi] [nvarchar](50) NOT NULL,
	[CauHoi] [nvarchar](50) NULL,
 CONSTRAINT [PK_ChiTietBaiThi] PRIMARY KEY CLUSTERED 
(
	[IDBaiThi] ASC,
	[IDCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTBTHS]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTBTHS](
	[IDBaiThiHS] [nvarchar](50) NOT NULL,
	[IDCauHoi] [nvarchar](50) NOT NULL,
	[CauTraLoi] [nvarchar](50) NULL,
	[DapAn] [nvarchar](50) NULL,
 CONSTRAINT [PK_CTBTHS] PRIMARY KEY CLUSTERED 
(
	[IDBaiThiHS] ASC,
	[IDCauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HocSinh]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HocSinh](
	[IDStudent] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Avatar] [nvarchar](max) NULL,
	[IDRole] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[IDStudent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[IDNhanVien] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Avatar] [nvarchar](max) NULL,
	[Address] [nvarchar](50) NULL,
	[IDRole] [int] NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[IDNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/23/2021 10:44:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IDRole] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BaiThi] ([IDBaiThi], [TenBaiThi], [ThoiGianLamBai]) VALUES (N'BT1', N'Đề toán 1', CAST(N'00:30:00' AS Time))
INSERT [dbo].[BaiThi] ([IDBaiThi], [TenBaiThi], [ThoiGianLamBai]) VALUES (N'BT2', N'Đề toán 2', CAST(N'00:30:00' AS Time))
INSERT [dbo].[BaiThi] ([IDBaiThi], [TenBaiThi], [ThoiGianLamBai]) VALUES (N'BT3', N'Đề toán 3', CAST(N'00:45:00' AS Time))
GO
INSERT [dbo].[BaiThiHS] ([IDBaiThiHS], [IDBaiThi], [ThoiGianLamBai], [TongSoDiem], [IDStudent]) VALUES (N'BTHS1', N'BT1', CAST(N'00:30:00' AS Time), N'3/5', N'HS0')
INSERT [dbo].[BaiThiHS] ([IDBaiThiHS], [IDBaiThi], [ThoiGianLamBai], [TongSoDiem], [IDStudent]) VALUES (N'BTHS2', N'BT2', CAST(N'00:29:00' AS Time), N'4/5', N'HS0')
INSERT [dbo].[BaiThiHS] ([IDBaiThiHS], [IDBaiThi], [ThoiGianLamBai], [TongSoDiem], [IDStudent]) VALUES (N'BTHS3', N'BT1', CAST(N'00:30:00' AS Time), N'3/3', N'HS1')
GO
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH1', N'1 + 2 = ?', N'1', N'2', N'3', N'4', N'C')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH2', N'5 * 2 = ?', N'2 ', N'10', N'5', N'12', N'B')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH3', N'9 - 4 = ?', N'5', N'-5', N'3', N'6', N'A')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH4', N'6 - 3 -2 = ? ', N'1', N'6 ', N'3', N'4', N'A')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH5', N'10 * 2 - 3 =?', N'7', N'19', N'13', N'17', N'D')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH6', N'13 + 12 = ?', N'23', N'1', N'25', N'-1', N'C')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH7', N'10 * 2 - 3 * 5 =?', N'10', N'20', N'15', N'5', N'A')
INSERT [dbo].[CauHoi] ([IDCauHoi], [CauHoi], [A], [B], [C], [D], [DapAn]) VALUES (N'CH8', N'10 * (2 - 3) * 5 = ?', N'50', N'-50', N'25', N'-25', N'B')
GO
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT1', N'CH1', N'1 + 2 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT1', N'CH2', N'5 * 2 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT1', N'CH3', N'9 - 4 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT1', N'CH4', N'6 - 3 -2 = ? ')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT1', N'CH5', N'10 * 2 - 3 =?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT2', N'CH2', N'5 * 2 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT2', N'CH4', N'6 - 3 -2 = ? ')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT2', N'CH5', N'10 * 2 - 3 =?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT3', N'CH1', N'1 + 2 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT3', N'CH2', N'5 * 2 = ?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT3', N'CH5', N'10 * 2 - 3 =?')
INSERT [dbo].[CTBT] ([IDBaiThi], [IDCauHoi], [CauHoi]) VALUES (N'BT3', N'CH8', N'10 * (2 - 3) * 5 = ?')
GO
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS1', N'CH1', N'A', N'C')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS1', N'CH2', N'B', N'B')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS1', N'CH3', N'C', N'A')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS1', N'CH4', N'A', N'A')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS1', N'CH5', N'D', N'D')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS2', N'CH1', N'C', N'C')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS2', N'CH2', N'D', N'B')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS2', N'CH3', N'A', N'A')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS2', N'CH5', N'D', N'D')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS3', N'CH2', N'B', N'B')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS3', N'CH4', N'A', N'A')
INSERT [dbo].[CTBTHS] ([IDBaiThiHS], [IDCauHoi], [CauTraLoi], [DapAn]) VALUES (N'BTHS3', N'CH5', N'D', N'D')
GO
INSERT [dbo].[HocSinh] ([IDStudent], [Email], [Password], [Name], [Avatar], [IDRole]) VALUES (N'HS0', N'giahuy@gmail.com', N'a953c428ceb5c5e8c0aba3f224edbd3f', N'Gia Huy', N'/Content/images/NguyenGiaHuy.jpg', 3)
INSERT [dbo].[HocSinh] ([IDStudent], [Email], [Password], [Name], [Avatar], [IDRole]) VALUES (N'HS1', N'ngoctrucaa@gmail.com', N'a953c428ceb5c5e8c0aba3f224edbd3f', N'Ngọc Trúc', N'/Content/images/avt-kh1.jpg', 3)
INSERT [dbo].[HocSinh] ([IDStudent], [Email], [Password], [Name], [Avatar], [IDRole]) VALUES (N'HS2', N'ngoctruc020100@gmail.com', N'6a4efc2dfafa4fe00bd2d7f6f2ff112a', N'Ngọc Trúc', N'/Content/images/avt-user1.jpg', 3)
GO
INSERT [dbo].[NhanVien] ([IDNhanVien], [Email], [Password], [Name], [Avatar], [Address], [IDRole]) VALUES (N'NV0', N'ngoctruc@gmail.com', N'21232f297a57a5a743894a0e4a801fc3', N'Ngọc Trúc', N'/Content/images/z2470613803310_0e91a6f408676ce0667c1a8bafbcba0a.jpg', N'123123', 1)
INSERT [dbo].[NhanVien] ([IDNhanVien], [Email], [Password], [Name], [Avatar], [Address], [IDRole]) VALUES (N'NV1', N'minhtam@gmail.com', N'2a2fa4fe2fa737f129ef2d127b861b7e', N'Tam', N'/Content/images/NguyenMinhTam.jpg', N'123', 2)
INSERT [dbo].[NhanVien] ([IDNhanVien], [Email], [Password], [Name], [Avatar], [Address], [IDRole]) VALUES (N'NV2', N'ngoctruc020100@gmail.com', N'f5fce1662295e09e104dc99a27978510', N'Ngọc Trúc', N'/Content/images/avt-kh1.jpg', N'123 la la la', 2)
INSERT [dbo].[NhanVien] ([IDNhanVien], [Email], [Password], [Name], [Avatar], [Address], [IDRole]) VALUES (N'NV3', N'ngoctruc111@gmail.com', N'c664b599d34a47b94667037f29bd3a61', N'Ngọc Trúc', N'/Content/Images/avatar-default-icon.png', N'123', 2)
INSERT [dbo].[NhanVien] ([IDNhanVien], [Email], [Password], [Name], [Avatar], [Address], [IDRole]) VALUES (N'NV4', N'ngoctruc123@gmail.com', N'21232f297a57a5a743894a0e4a801fc3', N'Ngọc Trúc', N'/Content/Images/avatar-default-icon.png', N'123', 2)
GO
INSERT [dbo].[Role] ([IDRole], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([IDRole], [Name]) VALUES (2, N'NhanVien')
INSERT [dbo].[Role] ([IDRole], [Name]) VALUES (3, N'HocSinh')
GO
ALTER TABLE [dbo].[BaiThiHS]  WITH CHECK ADD  CONSTRAINT [FK_BaiThiHS_BaiThi] FOREIGN KEY([IDBaiThi])
REFERENCES [dbo].[BaiThi] ([IDBaiThi])
GO
ALTER TABLE [dbo].[BaiThiHS] CHECK CONSTRAINT [FK_BaiThiHS_BaiThi]
GO
ALTER TABLE [dbo].[BaiThiHS]  WITH CHECK ADD  CONSTRAINT [FK_BaiThiHS_HocSinh] FOREIGN KEY([IDStudent])
REFERENCES [dbo].[HocSinh] ([IDStudent])
GO
ALTER TABLE [dbo].[BaiThiHS] CHECK CONSTRAINT [FK_BaiThiHS_HocSinh]
GO
ALTER TABLE [dbo].[CTBT]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietBaiThi_BaiThi] FOREIGN KEY([IDBaiThi])
REFERENCES [dbo].[BaiThi] ([IDBaiThi])
GO
ALTER TABLE [dbo].[CTBT] CHECK CONSTRAINT [FK_ChiTietBaiThi_BaiThi]
GO
ALTER TABLE [dbo].[CTBT]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietBaiThi_CauHoi] FOREIGN KEY([IDCauHoi])
REFERENCES [dbo].[CauHoi] ([IDCauHoi])
GO
ALTER TABLE [dbo].[CTBT] CHECK CONSTRAINT [FK_ChiTietBaiThi_CauHoi]
GO
ALTER TABLE [dbo].[HocSinh]  WITH CHECK ADD  CONSTRAINT [FK_Student_Role] FOREIGN KEY([IDRole])
REFERENCES [dbo].[Role] ([IDRole])
GO
ALTER TABLE [dbo].[HocSinh] CHECK CONSTRAINT [FK_Student_Role]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_Role] FOREIGN KEY([IDRole])
REFERENCES [dbo].[Role] ([IDRole])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_Role]
GO
