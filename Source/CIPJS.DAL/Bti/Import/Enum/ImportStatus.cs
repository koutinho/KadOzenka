namespace CIPJS.DAL.Bti.Import.Enum
{
    public enum ImportStatus
    {
        /// <summary>
        /// Зарезервировано таской для дальнейшего импорта 
        /// </summary>
        ReservedByTask = -1,

        /// <summary>
        /// Импорт удачно выполнен
        /// </summary>
        Success = 0,

        /// <summary>
        /// Импорт произошел с ошибкой
        /// </summary>
        Error = 1
    }
}
