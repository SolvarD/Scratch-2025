using Google.Cloud.SecretManager.V1;
using Microsoft.AspNetCore.DataProtection;

namespace Scratch2025.API.Services;

public interface ISecretManagerService {
	Task<string> GetSecretAsync(string key);
}
public class SecretManagerService: ISecretManagerService
{
	private readonly SecretManagerServiceClient _client;
	private readonly string _projectId;

	public SecretManagerService(IConfiguration configuration)
	{
		_client = SecretManagerServiceClient.Create();
		_projectId = configuration["Gcp:ProjectId"] ?? throw new ArgumentNullException("Gcp:ProjectId");
	}

	public async Task<string> GetSecretAsync(string key) {

		var secretName = new SecretVersionName(_projectId, key, "latest");
		var result = await _client.AccessSecretVersionAsync(secretName);

		return result.Payload.Data.ToStringUtf8();
	}
}
