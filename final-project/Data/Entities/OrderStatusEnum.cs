using System.ComponentModel;

namespace final_project.Data.Entities;

public enum OrderStatusEnum
{
    [Description("Обработва се")]
    Processing,
    [Description("Изпратена")]
    Sent,
    [Description("Получена")]
    Received
}