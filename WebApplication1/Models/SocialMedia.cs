namespace WebApplication1.Models;

public class SocialMedia : BaseEntity<int>
{
    public string FacebookLink { get; set; } = string.Empty;
    public string TwitterLink { get; set; } = string.Empty;
    public string PinterestLink { get; set; } = string.Empty;
    public string DribbbleLink { get; set; }= string.Empty;

}
