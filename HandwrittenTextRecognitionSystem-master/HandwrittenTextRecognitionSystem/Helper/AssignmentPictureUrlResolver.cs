using AutoMapper;
using AutoMapper.Execution;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Helper
{
    public class AssignmentPictureUrlResolver : IValueResolver<Assignment, AssignmentDto, string>
    {
        private readonly IConfiguration _configuration;

        public AssignmentPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Assignment source, AssignmentDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Path)) {
                return $"{_configuration["ApiBaseUrl"]}{source.Path}";
            }
            return string.Empty;
        }
    }
}
