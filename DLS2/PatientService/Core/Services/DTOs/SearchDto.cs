namespace PatientService.Core.Services.DTOs;

public class SearchDto : PaginationRequestDto
{
    public string SearchTerm { get; set; }
}