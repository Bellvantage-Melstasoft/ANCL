
class PrMaster {
    PrId;
    CompanyId;
    WarehouseId;
    PrType;
    PurchaseType;
    ExpectedDate;
    PurchaseProcedure;
    RequiredFor;
    PrCategoryId;
    PrSubCategoryId;
    ExpenseType;
    ExpenseRemarks;
    ISBudget;
    BudgetAmount;
    BudgetInfo;
    CreatedBy;
    PrDetails = [];
    PrCapexDocs = [];
    PrUpdateLog = {};
}

class PrDetails {
    PrdId;
    ItemId;
    ItemName;
    Description;
    EstimatedAmount;
    RequestedQty;
    MeasurementShortName;
    WarehouseStock;
    FileSampleProvided;
    Replacement;
    Remarks;
    IssuedQty;
    PrBoms = [];
    PrFileUploads = [];
    PrReplacementFileUploads = [];
    PrSupportiveDocuments = [];
    SparePartNo;

    Todo;
}

class PrBom {
    PrdId;
    Material;
    Description;

    Todo;
}

class PrFileUpload {
    PrdId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class PrReplacementFileUpload {
    PrdId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class PrSupportiveDocs {
    PrdId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class PrCapexDoc {
    PrId;
    FileName;
    FileData;
    FilePath;
    Todo;
}

class PrUpdateLog {
    UpdateRemarks;
}

