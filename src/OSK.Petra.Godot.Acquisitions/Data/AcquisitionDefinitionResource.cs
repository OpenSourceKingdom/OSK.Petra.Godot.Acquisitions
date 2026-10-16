using Godot;
using OSK.Petra.Acquisitions.Models;

namespace OSK.Petra.Godot.Acquisitions.Data;

/// <summary>
/// A configuration file that allows editing and setting acquisition definition data
/// </summary>
[GlobalClass]
[Tool]
public partial class AcquisitionDefinitionResource : Resource, IAcquisitionDefinition
{
	#region Variables

	private AcquireableResource _acquireableResource;
	private AcquisitionConfigurationResource _configuration;

	private AcquisitionDefinitionResource[] _choices;

	/// <summary>
	/// The main configuration for how the choice can be acquired
	/// </summary>
	[Export]
	private AcquisitionConfigurationResource ConfigurationResource
	{
		get => _configuration;
		set
		{
			_configuration = value;
			_configuration?.SetParentDependency(this);
		}
	}

	/// <summary>
	/// The actual thing that can be acquired
	/// </summary>
	[Export]
	private AcquireableResource AcquireableResource
	{
		get => _acquireableResource;
		set
		{
			_acquireableResource = value;
			if (_acquireableResource is not null)
			{
				_acquireableResource.Definition = this;
			}
		}
	}

	/// <summary>
	/// The collection of choices that follows this acquisition. Any choice in this collection implies and receives a dependency requirement on this choice
	/// </summary>
	[Export]
	protected AcquisitionDefinitionResource[] NextAcquistionChoices 
	{ 
		get => _choices;
		set
		{
			if (_choices is not null)
			{
				foreach (var choice in _choices.Where(c => c is not null))
				{
					if (value is null || !value.Contains(choice))
					{
						choice.SetParentDependency(null);
					}
				}
			}

			_choices = value;

			if (_choices is not null)
			{
				foreach (var child in _choices.Where(c => c is not null))
				{
					child.SetParentDependency(this);
				}
			}
		}
	}

	private readonly HashSet<IAcquisitionDefinition> _previousDefinitions = [];

	#endregion

	#region Api

	/// <summary>
	/// Sets the parent dependency for this definition resource
	/// </summary>
	/// <param name="resource">The definition resource that is the parent</param>
	public void SetParentDependency(AcquisitionDefinitionResource? resource)
	{
		if (resource == this)
		{
			_configuration?.SetParentDependency(null);
			return;
		}

		if (resource is null)
		{
			_previousDefinitions.Clear();
		}
		else
		{
			_previousDefinitions.Add(resource);
		}
		_configuration?.SetParentDependency(resource);
	}

	#endregion

	#region IAcquisitionDefinition

	/// <inheritdoc/>
	public AcquisitionChoiceConfiguration Configuration => _configuration.ToChoiceConfiguration();

    /// <inheritdoc/>
    public IAcquireable Acquireable => AcquireableResource;

    /// <inheritdoc/>
    public ICollection<IAcquisitionDefinition> NextChoices => _choices;

    /// <inheritdoc/>
    public ICollection<IAcquisitionDefinition> PreviousChoices => _previousDefinitions;

	#endregion
}
	 
