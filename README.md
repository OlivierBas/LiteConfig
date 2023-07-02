# LiteConfig
![lc.png](media\lc.png)

Simple configuration library to easily set up configuration files for projects

Usage:

```cs
var config = new LiteConfigBuilder()
                    .UseConfig("user")
                    .AddField(
                        new LiteConfigFieldBuilder()
                            .WithName("username")
                            .WithFieldType(FieldValueType.String)
                            .WithComment("Login Username\neg: admin")
                            .WithDefaultValue("name")
                            .Build()
                    ).Build();
```
Generated result:

![Generated result](\media\userlc.png)

Reading the value:
```cs
config.GetStringValue("username");
```

Multiple values:

```cs
var config = new LiteConfigBuilder()
                    .UseConfig("tokens")
                    .AddField(
                        new LiteConfigFieldBuilder()
                            .WithName("GITHUB_TOKEN")
                            .WithFieldType(FieldValueType.String)
                            .WithComment("Github Token")
                            .WithDefaultValue("YOUR_TOKEN_HERE")
                            .Build())
                    .AddField(
                        new LiteConfigFieldBuilder()
                            .WithName("OTHER_TOKEN")
                            .WithFieldType(FieldValueType.String)
                            .WithComment("Token for something else...")
                            .WithDefaultValue("YOUR_TOKEN_HERE")
                            .Build())
                    .Build();
```

![media\img.png](\media\tokenlc.png)

Reading the values:
```cs
config.GetStringValue("GITHUB_TOKEN");
config.GetStringValue("OTHER_TOKEN");
```