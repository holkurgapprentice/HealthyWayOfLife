using System;
using System.ComponentModel.DataAnnotations;
using HealthyWayOfLife.Model.Interfaces;

namespace HealthyWayOfLife.Model.Model.Database.Base
{
    public abstract class BaseDatabaseFieldInfo
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int InsertBy { get; set; }
        [Required]
        public int UpdateBy { get; set; }
        [Required]
        public DateTime InsertDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

        public TBaseClass CompleteDefaultValue<TBaseClass>() where TBaseClass : BaseDatabaseFieldInfo
        {
            InsertBy = 1;
            UpdateBy = 1;
            InsertDate = DateTime.Now;
            UpdateDate = DateTime.Now;

            return (TBaseClass)this;
        }

        public void CompleteInsertInformation(ISessionInformation sessionInformation)
        {
            Session session = sessionInformation?.GetSession();

            if (session?.User == null)
                return;
            
            InsertBy = UpdateBy = sessionInformation.GetSession().User.Id;
        }

        public void CompleteUpdateInformation(ISessionInformation sessionInformation)
        {
            Session session = sessionInformation?.GetSession();

            if (session?.User == null)
                return;

            UpdateBy = sessionInformation.GetSession().User.Id;

        }
    }
}