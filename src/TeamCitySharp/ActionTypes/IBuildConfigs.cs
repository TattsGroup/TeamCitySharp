using System;
using System.Collections.Generic;
using System.Xml;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCitySharp.ActionTypes
{
  public interface IBuildConfigs
  {
    List<BuildConfig> All();
    BuildConfigs GetFields(string fields);
    BuildConfig ByConfigurationName(string buildConfigName);
    BuildConfig ByConfigurationId(string buildConfigId);
    BuildConfig ByProjectNameAndConfigurationName(string projectName, string buildConfigName);
    BuildConfig ByProjectNameAndConfigurationId(string projectName, string buildConfigId);
    BuildConfig ByProjectIdAndConfigurationName(string projectId, string buildConfigName);
    BuildConfig ByProjectIdAndConfigurationId(string projectId, string buildConfigId);
    List<BuildConfig> ByProjectId(string projectId);
    List<BuildConfig> ByProjectName(string projectName);
    bool ModifTrigger(string buildTypeId, string triggerId, string newBt);
    BuildConfig CreateConfiguration(BuildConfig buildConfig);
    BuildConfig CreateConfiguration(string projectName, string configurationName);
    BuildConfig CreateConfigurationByProjectId(string projectId, string configurationName);

    BuildConfig Copy(string buildConfigId, string buildConfigName, string destinationProjectId,
                     string newBuildTypeId = "");


    void SetConfigurationSetting(IBuildTypeLocator locator, string settingName, string settingValue);
    bool GetConfigurationPauseStatus(IBuildTypeLocator locator);
    void SetConfigurationPauseStatus(IBuildTypeLocator locator, bool isPaused);


    void PostRawBuildStep(IBuildTypeLocator locator, string rawXml);
    void PostRawBuildTrigger(IBuildTypeLocator locator, string rawXml);
    void SetTrigger(IBuildTypeLocator locator, BuildTrigger trigger);

    void SetConfigurationParameter(IBuildTypeLocator locator, string key, string value);
    void PostRawAgentRequirement(IBuildTypeLocator locator, string rawXml);
    void DeleteBuildStep(IBuildTypeLocator locator, string buildStepId);

    void DeleteAgentRequirement(IBuildTypeLocator locator, string agentRequirementId);
    void DeleteParameter(IBuildTypeLocator locator, string parameterName);
    void DeleteBuildTrigger(IBuildTypeLocator locator, string buildTriggerId);

    /// <summary>
    /// DEPRECATED: After 2017.2 Please use AttachTemplates
    /// Makes a build type inherit a template.
    /// </summary>
    /// <param name="locatorBuildType">Locator for the build type which is to be associated with a template.</param>
    /// <param name="locatorTemplate">Locator for the template.</param>
    void SetBuildTypeTemplate(IBuildTypeLocator locatorBuildType, IBuildTypeLocator locatorTemplate);

    /// <summary>
    /// <para>Locates a build type by its locator.</para>
    /// <para>Essentially, it works either like <see cref="BuildConfigByConfigurationId"/> or <see cref="BuildConfigByConfigurationName"/>, whichever is defined in the locator.</para>
    /// </summary>
    /// <param name="locator">Locator for the build type.</param>
    /// <returns>The build type with all its properties.</returns>
    BuildConfig BuildType(IBuildTypeLocator locator);

    void SetBuildTypeVariable(IBuildTypeLocator locatorBuildType, string nameVariable, string value);

    void DeleteConfiguration(IBuildTypeLocator locator);

    /// <summary>
    /// Deletes all of the parameters defined locally on this build type.
    /// This spares those parameters inherited from the template, you will still get them when listing all parameters.
    /// </summary>
    /// <since>8.0</since>
    void DeleteAllBuildTypeParameters(IBuildTypeLocator locator);

    /// <summary>
    /// Replaces all of the parameters defined locally on this build type with the new set supplied.
    /// Same as calling <see cref="DeleteAllBuildTypeParameters"/> and then <see cref="SetConfigurationParameter"/> for each entry.
    /// </summary>
    /// <since>8.0</since>
    void PutAllBuildTypeParameters(IBuildTypeLocator locator, IDictionary<string, string> parameters);

    void DownloadConfiguration(IBuildTypeLocator locator, Action<string> downloadHandler);

    //Template
    Template CopyTemplate(string templateId, string templateName, string destinationProjectId, string newTemplateId = "");
    Template GetTemplate(IBuildTypeLocator locator);
    /// <summary>
    /// Supports version 2017.2 and higher
    /// </summary>
    /// <param name="locator"></param>
    /// <returns></returns>
    Templates GetTemplates(IBuildTypeLocator locator);
    void AttachTemplate(IBuildTypeLocator locator, string templateId);
    /// <summary>
    /// Supports version 2017.2 and higher
    /// </summary>
    /// <param name="locator"></param>
    /// <param name="templateList"></param>
    void AttachTemplates(IBuildTypeLocator locator, Templates templateList);
    void DetachTemplate(IBuildTypeLocator locator);
    /// <summary>
    /// Supports version 2017.2 and higher
    /// </summary>
    /// <param name="locator"></param>
    void DetachTemplates(IBuildTypeLocator locator);

    // Dependencies
    ArtifactDependencies GetArtifactDependencies(string buildTypeId);
    SnapshotDependencies GetSnapshotDependencies(string buildTypeId);
    
    /// <summary>
    /// <para>Adds a snapshot dependency to a build type. Have to post raw XML data which looks like this:</para>
    /// <code><![CDATA[
    /// <snapshot-dependency type="snapshot_dependency">
    ///        <properties>
    ///            <property name="source_buildTypeId" value="id-of-the-target-build-type"/>
    ///            <property name="run-build-if-dependency-failed" value="true"/>
    ///            <property name="run-build-on-the-same-agent" value="false"/>
    ///            <property name="take-started-build-with-same-revisions" value="true"/>
    ///            <property name="take-successful-builds-only" value="true"/>
    ///        </properties>
    ///    </snapshot-dependency>
    /// ]]></code>
    /// </summary>
    void PostRawSnapshotDependency(IBuildTypeLocator locator, XmlElement rawXml);
    void PostRawArtifactDependency(IBuildTypeLocator locator, string rawXml);
    void SetArtifactDependency(IBuildTypeLocator locator, ArtifactDependency dependency);
    void SetSnapshotDependency(IBuildTypeLocator locator, SnapshotDependency dependency);
    void DeleteArtifactDependency(IBuildTypeLocator locator, string artifactDependencyId);
    void DeleteSnapshotDependency(IBuildTypeLocator locator, string snapshotDependencyId);
    bool ModifArtifactDependencies(string format, string oldDendencyConfigurationId, string id);
    bool ModifSnapshotDependencies(string format, string oldDendencyConfigurationId, string id);
  }
}