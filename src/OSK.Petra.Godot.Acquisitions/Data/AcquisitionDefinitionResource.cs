using Godot;
using OSK.Petra.Acquisitions.Models;

namespace OSK.Petra.Godot.Acquisitions.Data;

[GlobalClass]
[Tool]
public partial class AcquisitionDefinitionResource : Resource, IAcquisitionDefinition
{
	#region Variables

	private AcquireableResource _acquireableResource;
	private AcquisitionConfigurationResource _configuration;

	private AcquisitionDefinitionResource[] _choices;

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

	public void SetParentDependency(AcquisitionDefinitionResource resource)
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

	public AcquisitionChoiceConfiguration Configuration => _configuration.ToChoiceConfiguration();

	public IAcquireable Acquireable => AcquireableResource;

	public ICollection<IAcquisitionDefinition> NextChoices => _choices;

	public ICollection<IAcquisitionDefinition> PreviousChoices => _previousDefinitions;

	#endregion
}
	 
