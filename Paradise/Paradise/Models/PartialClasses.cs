using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Paradise.Models
{
    [MetadataType(typeof(AdminMetadata))]
    public partial class Admin
    {
    }

    [MetadataType(typeof(DonorMetadata))]
    public partial class Donor
    {
    }

    [MetadataType(typeof(ErrorMetadata))]
    public partial class Error
    {
    }


    [MetadataType(typeof(PageMetadata))]
    public partial class Page
    {
    }

    [MetadataType(typeof(StaffMetadata))]
    public partial class Staff
    {
    }
}