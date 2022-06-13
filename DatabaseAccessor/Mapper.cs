using AutoMapper;
using DatabaseAccessor.Models;
using DatabaseAccessor.Resolvers;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Linq;

namespace DatabaseAccessor.Mapping
{
    public class Mapper
    {
        private static readonly Mapper _instance = null;

        private readonly IMapper _mapper;

        private Mapper()
        {
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<ShopProduct, MinimalProductDTO>()
                    .ForMember(target => target.Images,
                        options => options.MapFrom<ImageValueResolver>())
                    .ForMember(target => target.IsAvailable,
                        options => options.MapFrom(source => source.IsVisible && !source.IsDisabled));

                cfg.CreateMap<ShopProduct, ProductDTO>()
                    .IncludeBase<ShopProduct, MinimalProductDTO>()
                    .ForMember(target => target.CategoryName,
                        options => options.MapFrom(source => source.Category))
                    .ForMember(target => target.AverageRating,
                        options => options.MapFrom(source => source.Comments.Average(comment => comment.Star) ?? 0))
                    .ForMember(target => target.IsNewProduct,
                        options => options.MapFrom(source => DateTime.Now.AddHours(7).AddDays(3) >= source.CreatedDate));

                cfg.CreateMap<ShopProduct, ProductWithCommentsDTO>()
                    .IncludeBase<ShopProduct, ProductDTO>();

                cfg.CreateMap<ShopInterface, ShopInterfaceDTO>()
                    .ForMember(target => target.Images,
                        options => options.MapFrom<ImageValueResolver>());

                cfg.CreateMap<CartDetail, CartItemDTO>()
                    .ForMember(target => target.ProductName,
                        options => options.MapFrom(source => source.Product.ProductName))
                    .ForMember(target => target.Price,
                        options => options.MapFrom(source => source.Product.Price))
                    .ForMember(target => target.Discount,
                        options => options.MapFrom(source => source.Product.Discount))
                    .ForMember(target => target.Image,
                        options => options.MapFrom<SingleImageResolver>())
                    .ForMember(target => target.IsAvailable,
                        options => options.MapFrom(source => source.Product.IsVisible && !source.Product.IsDisabled));

                cfg.CreateMap<ProductComment, RatingDTO>()
                    .ForMember(target => target.ProductName,
                        option => option.MapFrom(source => source.Product.ProductName))
                    .ForMember(target => target.UserName,
                        option => option.MapFrom(source => source.User.UserName));

                cfg.CreateMap<Report, ReportDTO>()
                    .ForMember(target => target.Reporter,
                        options => options.MapFrom(source => source.Reporter.UserName))
                    .ForMember(target => target.AffectedUser,
                        options => options.MapFrom(source => source.AffectedUser.UserName));

                cfg.CreateMap<User, UserDTO>()
                    .ForMember(target => target.BirthDay,
                        options => options.MapFrom(source => source.DoB))
                    .ForMember(target => target.IsConfirmed,
                        options => options.MapFrom(source => source.EmailConfirmed))
                    .ForMember(target => target.FullName,
                        options => options.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                    .ForMember(target => target.IsLockedOut,
                        options => options.MapFrom(source => source.LockoutEnd > DateTimeOffset.Now
                            && source.Status == AccountStatus.Available))
                    .ForMember(target => target.IsAvailable,
                        options => options.MapFrom(source => source.Status == AccountStatus.Available))
                    .ForMember(target => target.Role,
                        options => options.MapFrom(source => source.UserRoles[0].Role.Name))
                    .ForMember(target => target.ReportCount,
                        options => options.MapFrom(source => source.AffectedReports.Count));

                cfg.CreateMap<Invoice, InvoiceDTO>()
                    .ForMember(target => target.InvoiceId,
                        options => options.MapFrom(source => source.Id))
                    .ForMember(target => target.ReceiverName,
                        options => options.MapFrom(source => source.FullName))
                    .ForMember(target => target.PhoneNumber,
                        options => options.MapFrom(source => source.Phone));

                cfg.CreateMap<Invoice, InvoiceWithReportDTO>()
                    .IncludeBase<Invoice, InvoiceDTO>()
                    .ForMember(target => target.IsReported,
                        options => options.MapFrom(source => source.Report != null));

                cfg.CreateMap<Invoice, InvoiceWithItemDTO>()
                    .IncludeBase<Invoice, InvoiceDTO>()
                    .ForMember(target => target.Products,
                        options => options.MapFrom(source => source.Details));

                cfg.CreateMap<Invoice, FullInvoiceDTO>()
                    .IncludeBase<Invoice, InvoiceWithItemDTO>()
                    .ForMember(target => target.StatusHistories,
                        options => options.MapFrom(source => source.StatusChangedHistories));

                cfg.CreateMap<InvoiceDetail, InvoiceItemDTO>()
                    .ForMember(target => target.Image,
                        options => options.MapFrom<SingleImageResolver>())
                    .ForMember(target => target.ProductName,
                        options => options.MapFrom(source => source.Product.ProductName))
                    .ForMember(target => target.CanBeRating,
                        options => options.MapFrom(source => !source.IsRated && source.Invoice.Status == InvoiceStatus.Succeed));

                cfg.CreateMap<InvoiceStatusChangedHistory, InvoiceStatusChangedHistoryDTO>()
                    .ForMember(target => target.ChangedTime,
                        options => options.MapFrom(source => source.ChangedDate))
                    .ForMember(target => target.Status,
                        options => options.MapFrom(source => source.NewStatus));
            });
            _mapper = config.CreateMapper();
        }

        public static Mapper GetInstance()
        {
            return _instance ?? new Mapper();
        }

        public ProductDTO MapToProductDTO(ShopProduct product) => _mapper.Map<ProductDTO>(product);

        public MinimalProductDTO MapToMinimalProductDTO(ShopProduct product) => _mapper.Map<MinimalProductDTO>(product);

        public ProductWithCommentsDTO MapToProductWithCommentsDTO(ShopProduct product) => _mapper.Map<ProductWithCommentsDTO>(product);

        public ShopInterfaceDTO MapToShopInterfaceDTO(ShopInterface shopInterface)
            => _mapper.Map<ShopInterfaceDTO>(shopInterface);

        public CartItemDTO MapToCartItemDTO(CartDetail cartItem) => _mapper.Map<CartItemDTO>(cartItem);

        public RatingDTO MapToRatingDTO(ProductComment rating) => _mapper.Map<RatingDTO>(rating);

        public ReportDTO MapToReportDTO(Report report) => _mapper.Map<ReportDTO>(report);

        public UserDTO MapToUserDTO(User user) => _mapper.Map<UserDTO>(user);

        public InvoiceDTO MapToInvoiceDTO(Invoice invoice) => _mapper.Map<InvoiceDTO>(invoice);

        public InvoiceWithReportDTO MapToInvoiceWithReportDTO(Invoice invoice) => _mapper.Map<InvoiceWithReportDTO>(invoice);

        public InvoiceWithItemDTO MapToInvoiceWithItemDTO(Invoice invoice) => _mapper.Map<InvoiceWithItemDTO>(invoice);

        public FullInvoiceDTO MapToFullInvoiceDTO(Invoice invoice) => _mapper.Map<FullInvoiceDTO>(invoice);
    }
}
