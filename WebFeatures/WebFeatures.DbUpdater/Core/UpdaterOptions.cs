namespace WebFeatures.DbUpdater.Core
{
    /// <summary>
    /// Параметры обновления БД
    /// </summary>
    class UpdaterOptions
    {
        /// <summary>
        /// Пересоздать
        /// </summary>
        public bool Recreate { get; set; }

        /// <summary>
        /// Принять миграцию
        /// </summary>
        public bool Migrate { get; set; }
    }
}
