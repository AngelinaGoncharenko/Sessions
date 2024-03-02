namespace DbClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        middleName = c.String(),
                        lastName = c.String(),
                        login = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AppointmentEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        lastDate = c.DateTime(nullable: false),
                        NextDate = c.DateTime(nullable: false),
                        diagnostics_id = c.Int(),
                        doctor_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiagnosticsEntities", t => t.diagnostics_id)
                .ForeignKey("dbo.DoctorEntities", t => t.doctor_id)
                .Index(t => t.diagnostics_id)
                .Index(t => t.doctor_id);
            
            CreateTable(
                "dbo.DiagnosticsEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateBegin = c.DateTime(nullable: false),
                        results = c.String(),
                        recomendations = c.String(),
                        doctor_id = c.Int(),
                        events_id = c.Int(),
                        patient_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DoctorEntities", t => t.doctor_id)
                .ForeignKey("dbo.EventEntities", t => t.events_id)
                .ForeignKey("dbo.PatientEntities", t => t.patient_id)
                .Index(t => t.doctor_id)
                .Index(t => t.events_id)
                .Index(t => t.patient_id);
            
            CreateTable(
                "dbo.DoctorEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        account_id = c.Int(),
                        profession_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AccountEntities", t => t.account_id)
                .ForeignKey("dbo.ProfessionEntities", t => t.profession_id)
                .Index(t => t.account_id)
                .Index(t => t.profession_id);
            
            CreateTable(
                "dbo.ProfessionEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.EventEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        typeEvent_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.TypeEntities", t => t.typeEvent_id)
                .Index(t => t.typeEvent_id);
            
            CreateTable(
                "dbo.TypeEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PatientEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        middleName = c.String(),
                        lastName = c.String(),
                        pasport = c.String(),
                        birthday = c.DateTime(nullable: false),
                        address = c.String(),
                        number = c.String(),
                        email = c.String(),
                        policy = c.Int(nullable: false),
                        policyDate = c.DateTime(nullable: false),
                        workPlace = c.String(),
                        insuranceCompany = c.String(),
                        gender_Id = c.Int(),
                        medcard_id = c.Int(),
                        paiment_Id = c.Int(),
                        typeInsuranceCompany_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.GenderEntities", t => t.gender_Id)
                .ForeignKey("dbo.MedCardEntities", t => t.medcard_id)
                .ForeignKey("dbo.PaimentEntities", t => t.paiment_Id)
                .ForeignKey("dbo.TypeInsuranceEntities", t => t.typeInsuranceCompany_Id)
                .Index(t => t.gender_Id)
                .Index(t => t.medcard_id)
                .Index(t => t.paiment_Id)
                .Index(t => t.typeInsuranceCompany_Id);
            
            CreateTable(
                "dbo.GenderEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedCardEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dateCreateMedCard = c.DateTime(nullable: false),
                        photoPatients = c.String(),
                        appointment_Id = c.Int(),
                        medHistory_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AppointmentEntities", t => t.appointment_Id)
                .ForeignKey("dbo.MedHistoryEntities", t => t.medHistory_id)
                .Index(t => t.appointment_Id)
                .Index(t => t.medHistory_id);
            
            CreateTable(
                "dbo.MedHistoryEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        dateOfRecovery = c.DateTime(nullable: false),
                        anamnesis = c.String(),
                        symptoms = c.String(),
                        recomendations = c.String(),
                        diagnosis_id = c.Int(),
                        recept_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.DiagnosisEntities", t => t.diagnosis_id)
                .ForeignKey("dbo.ReceptEntities", t => t.recept_id)
                .Index(t => t.diagnosis_id)
                .Index(t => t.recept_id);
            
            CreateTable(
                "dbo.DiagnosisEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ReceptEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        dosage = c.String(),
                        format = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PaimentEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeInsuranceEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConditionEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DepartmentEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.HospitalizationEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dateHospitalozation = c.DateTime(nullable: false),
                        target = c.String(),
                        informations = c.String(),
                        dateEndHospitalization = c.DateTime(nullable: false),
                        condition_id = c.Int(),
                        department_id = c.Int(),
                        medCard_id = c.Int(),
                        patient_id = c.Int(),
                        status_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ConditionEntities", t => t.condition_id)
                .ForeignKey("dbo.DepartmentEntities", t => t.department_id)
                .ForeignKey("dbo.MedCardEntities", t => t.medCard_id)
                .ForeignKey("dbo.PatientEntities", t => t.patient_id)
                .ForeignKey("dbo.StatusEntities", t => t.status_id)
                .Index(t => t.condition_id)
                .Index(t => t.department_id)
                .Index(t => t.medCard_id)
                .Index(t => t.patient_id)
                .Index(t => t.status_id);
            
            CreateTable(
                "dbo.StatusEntities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HospitalizationEntities", "status_id", "dbo.StatusEntities");
            DropForeignKey("dbo.HospitalizationEntities", "patient_id", "dbo.PatientEntities");
            DropForeignKey("dbo.HospitalizationEntities", "medCard_id", "dbo.MedCardEntities");
            DropForeignKey("dbo.HospitalizationEntities", "department_id", "dbo.DepartmentEntities");
            DropForeignKey("dbo.HospitalizationEntities", "condition_id", "dbo.ConditionEntities");
            DropForeignKey("dbo.AppointmentEntities", "doctor_id", "dbo.DoctorEntities");
            DropForeignKey("dbo.AppointmentEntities", "diagnostics_id", "dbo.DiagnosticsEntities");
            DropForeignKey("dbo.DiagnosticsEntities", "patient_id", "dbo.PatientEntities");
            DropForeignKey("dbo.PatientEntities", "typeInsuranceCompany_Id", "dbo.TypeInsuranceEntities");
            DropForeignKey("dbo.PatientEntities", "paiment_Id", "dbo.PaimentEntities");
            DropForeignKey("dbo.PatientEntities", "medcard_id", "dbo.MedCardEntities");
            DropForeignKey("dbo.MedCardEntities", "medHistory_id", "dbo.MedHistoryEntities");
            DropForeignKey("dbo.MedHistoryEntities", "recept_id", "dbo.ReceptEntities");
            DropForeignKey("dbo.MedHistoryEntities", "diagnosis_id", "dbo.DiagnosisEntities");
            DropForeignKey("dbo.MedCardEntities", "appointment_Id", "dbo.AppointmentEntities");
            DropForeignKey("dbo.PatientEntities", "gender_Id", "dbo.GenderEntities");
            DropForeignKey("dbo.DiagnosticsEntities", "events_id", "dbo.EventEntities");
            DropForeignKey("dbo.EventEntities", "typeEvent_id", "dbo.TypeEntities");
            DropForeignKey("dbo.DiagnosticsEntities", "doctor_id", "dbo.DoctorEntities");
            DropForeignKey("dbo.DoctorEntities", "profession_id", "dbo.ProfessionEntities");
            DropForeignKey("dbo.DoctorEntities", "account_id", "dbo.AccountEntities");
            DropIndex("dbo.HospitalizationEntities", new[] { "status_id" });
            DropIndex("dbo.HospitalizationEntities", new[] { "patient_id" });
            DropIndex("dbo.HospitalizationEntities", new[] { "medCard_id" });
            DropIndex("dbo.HospitalizationEntities", new[] { "department_id" });
            DropIndex("dbo.HospitalizationEntities", new[] { "condition_id" });
            DropIndex("dbo.MedHistoryEntities", new[] { "recept_id" });
            DropIndex("dbo.MedHistoryEntities", new[] { "diagnosis_id" });
            DropIndex("dbo.MedCardEntities", new[] { "medHistory_id" });
            DropIndex("dbo.MedCardEntities", new[] { "appointment_Id" });
            DropIndex("dbo.PatientEntities", new[] { "typeInsuranceCompany_Id" });
            DropIndex("dbo.PatientEntities", new[] { "paiment_Id" });
            DropIndex("dbo.PatientEntities", new[] { "medcard_id" });
            DropIndex("dbo.PatientEntities", new[] { "gender_Id" });
            DropIndex("dbo.EventEntities", new[] { "typeEvent_id" });
            DropIndex("dbo.DoctorEntities", new[] { "profession_id" });
            DropIndex("dbo.DoctorEntities", new[] { "account_id" });
            DropIndex("dbo.DiagnosticsEntities", new[] { "patient_id" });
            DropIndex("dbo.DiagnosticsEntities", new[] { "events_id" });
            DropIndex("dbo.DiagnosticsEntities", new[] { "doctor_id" });
            DropIndex("dbo.AppointmentEntities", new[] { "doctor_id" });
            DropIndex("dbo.AppointmentEntities", new[] { "diagnostics_id" });
            DropTable("dbo.StatusEntities");
            DropTable("dbo.HospitalizationEntities");
            DropTable("dbo.DepartmentEntities");
            DropTable("dbo.ConditionEntities");
            DropTable("dbo.TypeInsuranceEntities");
            DropTable("dbo.PaimentEntities");
            DropTable("dbo.ReceptEntities");
            DropTable("dbo.DiagnosisEntities");
            DropTable("dbo.MedHistoryEntities");
            DropTable("dbo.MedCardEntities");
            DropTable("dbo.GenderEntities");
            DropTable("dbo.PatientEntities");
            DropTable("dbo.TypeEntities");
            DropTable("dbo.EventEntities");
            DropTable("dbo.ProfessionEntities");
            DropTable("dbo.DoctorEntities");
            DropTable("dbo.DiagnosticsEntities");
            DropTable("dbo.AppointmentEntities");
            DropTable("dbo.AccountEntities");
        }
    }
}
