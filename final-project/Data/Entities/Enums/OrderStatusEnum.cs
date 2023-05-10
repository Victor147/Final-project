using System.ComponentModel;

namespace final_project.Data.Entities.Enums;

public enum OrderStatusEnum
{
    [Description("Изчаква потвърждение")]
    Waiting,
    [Description("Обработва се")]
    Processing,
    [Description("Изпратена")]
    Sent,
    [Description("Получена")]
    Received
}