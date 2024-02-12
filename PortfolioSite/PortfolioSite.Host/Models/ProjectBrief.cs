namespace PortfolioSite.Host.Models;

public record ProjectBrief(string Title, string Description, string[] TechStack, string? RepoLink, string? DemoLink, string? LiveSite);

