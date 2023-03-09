# Cognito Oidc Cli
[![Actions Status](https://github.com/ZenExtensions/cognito-oidc-cli/workflows/.NET%20Core%20Publish/badge.svg)](https://github.com/ZenExtensions/cognito-oidc-cli/actions) [![Current Version](https://img.shields.io/badge/Version-1.0.0-brightgreen?logo=nuget&labelColor=30363D)](./CHANGELOG.md#100--2023-03-10)

Utility command line for [Amazon Cognito user pool OIDC](https://docs.aws.amazon.com/cognito/latest/developerguide/cognito-userpools-server-contract-reference.html).

# Overview
## Installation
Installing is as simple as running this command 🤟
```bash
dotnet tool install --global cognito-oidc
```
If you need to update the cli, run the following command
```bash
dotnet tool update --global cognito-oidc
```
You need to have .net 6 runtime installed on your system for this.

## Usage

```shell
cognito-oidc --help
```

### Get Access Token
To get access token, use the following command
```shell
cognito-oidc client-credentials --domain "" --cognito-id "" --cognito-secret ""
```
