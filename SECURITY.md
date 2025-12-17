# Security Policy

## Supported Versions

We release patches for security vulnerabilities. Which versions are eligible for receiving such patches depends on the CVSS v3.0 Rating:

| Version | Supported          |
| ------- | ------------------ |
| Latest  | :white_check_mark: |
| < Latest| :x:                |

## Reporting a Vulnerability

Please report (suspected) security vulnerabilities to [GitHub Security Advisory](https://github.com/PhanCongVuDuc/Sonny/security/advisories/new). You will receive a response within 48 hours. If the issue is confirmed, we will release a patch as soon as possible depending on complexity but historically within a few days.

### What to Include

When reporting a security vulnerability, please include:

- **Type of issue** (e.g., buffer overflow, SQL injection, cross-site scripting, etc.)
- **Full paths of source file(s) related to the manifestation of the issue**
- **The location of the affected source code** (tag/branch/commit or direct URL)
- **Step-by-step instructions to reproduce the issue**
- **Proof-of-concept or exploit code** (if possible)
- **Impact of the issue**, including how an attacker might exploit the issue

This information will help us triage your report more quickly.

## Security Best Practices

When using Sonny:

- Always use the latest version
- Review the code before deploying in production environments
- Follow Revit API best practices
- Keep your Revit installation updated
- Use proper authentication and authorization in your implementations

## Disclosure Policy

- When we receive a security bug report, we will assign it to a primary handler. This person will coordinate the fix and release process.
- We will confirm the issue and determine the affected versions.
- We will audit the codebase to find any similar problems.
- We will prepare fixes for all releases still under support. These fixes will be released as quickly as possible.

## Recognition

We recognize security researchers who responsibly disclose vulnerabilities. If you would like to be credited, please let us know how you would like to be recognized (name, handle, organization, etc.).

Thank you for helping keep Sonny and our community safe!

