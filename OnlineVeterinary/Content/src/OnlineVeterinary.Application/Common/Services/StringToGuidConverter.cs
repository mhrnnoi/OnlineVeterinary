namespace OnlineVeterinary.Application.Common.Services
{
    public static class StringToGuidConverter
    {
        public static Guid ConvertToGuid(string id)
        {
            return Guid.Parse(id);
        }
    }
}