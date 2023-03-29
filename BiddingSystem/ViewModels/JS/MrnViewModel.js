
class MrnMaster {
    MrnId;
    CompanyId;
    SubDepartmentId;
    WarehouseId;
    MrnType;
    PurchaseType;
    ExpectedDate;
    PurchaseProcedure;
    RequiredFor;
    MrnCategoryId;
    MrnSubCategoryId;
    ExpenseType;
    ExpenseRemarks;
    ISBudget;
    BudgetAmount;
    BudgetInfo;
    CreatedBy;
    MrnDetails = [];
    MrnCapexDocs = [];
    MrnUpdateLog = {};
}

class MrnDetails {
    MrndId;
    ItemId;
    ItemName;
    Description;
    EstimatedAmount;
    RequestedQty;
    MeasurementShortName;
    DepartmentStock;
    FileSampleProvided;
    Replacement;
    Remarks;
    IssuedQty;
    MrnBoms = [];
    MrnFileUploads = [];
    MrnReplacementFileUploads = [];
    MrnSupportiveDocuments = [];
    SparePartNo;

    Todo;
}

class MrnBom {
    MrndId;
    Material;
    Description;
    Todo;
    IsAdded;
    ItemId;
}

class MrnFileUpload {
    MrndId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class MrnReplacementFileUpload {
    MrndId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class MrnSupportiveDocs {
    MrndId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class MrnCapexDoc {
    MrnId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class MrnUpdateLog {
    UpdateRemarks;
}

class BOMDetails {
    Material;
    Description;
    ItemId;

}