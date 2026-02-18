---
name: Dev Mode
description: Toggle the repo between development mode (project references) and release mode (NuGet package references).
tools: ['editFiles', 'codebase', 'terminal']
---

# Development / Release Mode Agent

Toggle the repository between **development mode** and **release mode** for the `Atmoos.Quantities` project family.

## Purpose

When developing new features or quantities in `Atmoos.Quantities`, downstream projects (`Atmoos.Quantities.Units`, `Atmoos.Quantities.Serialization.Text.Json`, `Atmoos.Quantities.Serialization.Newtonsoft`) reference it as a **NuGet package**. This means changes to `Atmoos.Quantities` are invisible to those projects until a release is published.

This agent switches the three downstream `.csproj` files between:

- **Development mode**: `ProjectReference` — enables rapid prototyping with unreleased changes.
- **Release mode**: `PackageReference` — restores the NuGet dependency for clean packaging.

## Instructions

When the user asks to switch to **development mode** or **release mode**, follow the steps below.

### Identifying the Current Mode

Inspect the three target files for the presence of `PackageReference` or `ProjectReference` to `Atmoos.Quantities`:

| File | Path |
|------|------|
| Units | `source/Atmoos.Quantities.Units/Atmoos.Quantities.Units.csproj` |
| Text.Json | `source/Atmoos.Quantities.Serialization/Text.Json/Atmoos.Quantities.Serialization.Text.Json.csproj` |
| Newtonsoft | `source/Atmoos.Quantities.Serialization/Newtonsoft/Atmoos.Quantities.Serialization.Newtonsoft.csproj` |

- If they contain `<PackageReference Include="Atmoos.Quantities" ... />` → currently in **release mode**.
- If they contain `<ProjectReference Include="...Atmoos.Quantities.csproj" />` → currently in **development mode**.

Report the current mode to the user before making changes.

---

### Switching to Development Mode

Replace every `PackageReference` to `Atmoos.Quantities` with the corresponding `ProjectReference` in the three files listed above.

#### Replacements

**`source/Atmoos.Quantities.Units/Atmoos.Quantities.Units.csproj`**

Replace:
```xml
<PackageReference Include="Atmoos.Quantities" Version="2.2.0" />
```
With:
```xml
<ProjectReference Include="..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```

**`source/Atmoos.Quantities.Serialization/Text.Json/Atmoos.Quantities.Serialization.Text.Json.csproj`**

Replace:
```xml
<PackageReference Include="Atmoos.Quantities" Version="2.2.0" />
```
With:
```xml
<ProjectReference Include="..\..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```

**`source/Atmoos.Quantities.Serialization/Newtonsoft/Atmoos.Quantities.Serialization.Newtonsoft.csproj`**

Replace:
```xml
<PackageReference Include="Atmoos.Quantities" Version="2.2.0" />
```
With:
```xml
<ProjectReference Include="..\..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```

#### Important

- The version number in the `PackageReference` may differ from `2.2.0`. Match whatever version is present.
- Do **not** modify any other `PackageReference` entries (e.g., `Newtonsoft.Json`).
- Only replace the `Atmoos.Quantities` reference, nothing else.

---

### Switching to Release Mode

Reverse the development mode changes: replace every `ProjectReference` to `Atmoos.Quantities.csproj` with the corresponding `PackageReference`.

#### Determining the Version

Before making changes, read the version from `source/Atmoos.Quantities/Atmoos.Quantities.csproj`:

```xml
<Version>2.2.0</Version>
```

Use this version for all `PackageReference` entries to ensure consistency.

#### Replacements

**`source/Atmoos.Quantities.Units/Atmoos.Quantities.Units.csproj`**

Replace:
```xml
<ProjectReference Include="..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```
With:
```xml
<PackageReference Include="Atmoos.Quantities" Version="{version}" />
```

**`source/Atmoos.Quantities.Serialization/Text.Json/Atmoos.Quantities.Serialization.Text.Json.csproj`**

Replace:
```xml
<ProjectReference Include="..\..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```
With:
```xml
<PackageReference Include="Atmoos.Quantities" Version="{version}" />
```

**`source/Atmoos.Quantities.Serialization/Newtonsoft/Atmoos.Quantities.Serialization.Newtonsoft.csproj`**

Replace:
```xml
<ProjectReference Include="..\..\Atmoos.Quantities\Atmoos.Quantities.csproj" />
```
With:
```xml
<PackageReference Include="Atmoos.Quantities" Version="{version}" />
```

Where `{version}` is the value read from `source/Atmoos.Quantities/Atmoos.Quantities.csproj`.

---

## Verification

After making changes, **always** build the solution to confirm everything compiles:

```bash
dotnet build source/Quantities.sln
```

Report the result to the user.

---

## Safety Rules

1. **Never** modify `source/Atmoos.Quantities/Atmoos.Quantities.csproj` itself.
2. **Never** change references other than `Atmoos.Quantities` (e.g., `Newtonsoft.Json`, `Atmoos.Sphere`).
3. **Never** change version numbers in `<PropertyGroup>` — only the `PackageReference` version attribute.
4. If any of the three files are already in the target mode, skip them and inform the user.
5. Always report which files were changed and which were already in the correct state.
