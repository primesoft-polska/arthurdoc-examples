## ArthurDoc - Java Client

Example Java application for requesting ArthurDoc API.

### Build

```console
mvn clean install
```

### Config

Set authorization token and API address in file: src\main\resources\config.properties.

### Run application

The following arguments need to be passed:
- name
- templateguid
- file (as plain text or as a path to file in the following format: "path:C:\file.json")
- merge_pdf

```console
java -jar .\target\ArthurDocJavaClient.jar <name> <templateguid> <file> <merge_pdf>
```