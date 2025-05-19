# discover-profile-notification-csharp

<!-- ![Build](https://github.com/discoverswiss/profile-notification-csharp-example/actions/workflows/azure-function-csharp.yml/badge.svg) -->
![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)

A C# Azure Function example demonstrating how to receive and process **profile notification events** from the [discover.swiss](https://discover.swiss) platform using **Azure Service Bus Queue messages**.

This sample helps developers integrate event-driven solutions using Azure Functions that respond to user profile changes — such as creation, updates, or deletions — delivered by the discover.swiss Profile Notification Service.

---

## 🚀 Features

- Azure Function triggered by Service Bus Queue messages
- Demonstrates handling of discover.swiss profile notification events
- Covers multiple use cases (e.g. profile created, ticket cancelled, order updated)
- Secure environment variable configuration
- Lightweight and ready for use as a starting point

---

## 📚 Documentation

👉 **Official Service Documentation**  
For detailed information about how the profile notification system works, visit:  
🔗 [https://docs.discover.swiss/dev/concepts/profile/profile-notifications/](https://docs.discover.swiss/dev/concepts/profile/profile-notifications/)

---

## 🧩 Prerequisites

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download)
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Azure Subscription (for deployment)
- Access to a configured Azure Service Bus queue
- discover.swiss Profile Notification Service access

---

## 🛠️ Getting Started

1. **Clone this repository**

```bash
git clone https://github.com/discoverswiss/profile-notification-csharp-example.git
cd profile-notification-csharp-example
```

2. **Set up local configuration**

Create a `local.settings.json` file in the root directory. This file contains environment variables needed for running the function locally and **is excluded from version control** by `.gitignore`.

Here’s an example you can use:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "ServiceBusConnectionString": "<your-connection-string>"
  }
}
```

> ⚠️ Replace `<your-connection-string>` with your Azure Service Bus connection string containing access to the queue that receives profile notifications.

3. **Run locally**

```bash
func start
```

4. **Deploy to Azure (optional)**

```bash
func azure functionapp publish <YourFunctionAppName>
```

---

## ✅ Contributing

Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for details.

If you encounter issues or have feature requests, [open an issue](https://github.com/discoverswiss/profile-notification-csharp-example/issues).

---

## 🔒 Security

Please report vulnerabilities to:  
📧 **support@discover.swiss**

Refer to [SECURITY.md](SECURITY.md) for details.

---

## 📜 License

This example is provided under the [MIT License](LICENSE).

---

## 🙌 Support

If you need help or have questions, contact our team at  
📧 **support@discover.swiss**
