namespace Messaging.SharedMessages;

public class DeletePatientMessage
{
    public string Message { get; set; }
    public string SSN { get; set; }

    public DeletePatientMessage(string message, string ssn)
    {
        Message = message;
        SSN = ssn;
    }
}