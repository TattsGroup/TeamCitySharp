using Newtonsoft.Json;

namespace TeamCitySharp.DomainEntities
{
  public class Template
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("href")]
    public string Href { get; set; }

    [JsonProperty("projectId")]
    public string ProjectId { get; set; }

    [JsonProperty("projectName")]
    public string ProjectName { get; set; }
	
    [JsonProperty("project")]
    public Project Project { get; set; }

    [JsonProperty("parameters")]
    public Parameters Parameters { get; set; }

    [JsonProperty("artifact-dependencies")]
    public ArtifactDependencies ArtifactDependencies { get; set; }

    [JsonProperty("snapshot-dependencies")]
    public SnapshotDependencies SnapshotDependencies { get; set; }

    [JsonProperty("vcs-root-entries")]
    public VcsRootEntries VcsRootEntries { get; set; }

    [JsonProperty("steps")]
    public BuildSteps Steps { get; set; }

    [JsonProperty("agent-requirements")]
    public AgentRequirements AgentRequirements { get; set; }

    [JsonProperty("features")]
    public BuildFeatures Features { get; set; }

    [JsonProperty("triggers")]
    public BuildTriggers Triggers { get; set; }

    [JsonProperty("settings")]
    public Properties Settings { get; set; }
  }
}