//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practic5.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employees()
        {
            this.users = new HashSet<users>();
        }

        public long idEmployee { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string lastName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string firstName { get; set; }
        [StringLength(30, MinimumLength = 2)]
        public string middleName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime birthDate { get; set; }
        [Required]
        public long positionAtWork { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal wages { get; set; }
        [Required]
        [StringLength(12)]
        public string phoneNumber { get; set; }

        public virtual jobTitles jobTitles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users> users { get; set; }

        public string fullName
        {
            get
            {
                return $"{lastName} {firstName} {middleName}";
            }
        }
    }
}
