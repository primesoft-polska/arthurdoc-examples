package pl.primesoft.arthurdoc;

import java.io.*;
import java.nio.charset.StandardCharsets;

public class RequestParams {
    private String name;
    private String templateguid;
    private String file;
    private String merge_pdf;

    RequestParams(String name, String guid, String file, String merge) throws IOException {
        this.name = name;
        this.templateguid = guid;
        setFile(file);
        this.merge_pdf = merge;
    }
    public void setName(String name) {
        this.name = name;
    }

    public void setTemplateguid(String templateguid) {
        this.templateguid = templateguid;
    }

    public void setFile(String file) throws IOException {
        if (file.length() > 5 && file.substring(0,5).equals("path:")) {
            StringBuilder builder = new StringBuilder();
            try  (BufferedReader reader = new BufferedReader(
                    new InputStreamReader(new FileInputStream(file.substring(5)), StandardCharsets.UTF_8))) {
                String line;
                while ((line = reader.readLine()) != null) {
                    builder.append(line);
                }
            }
            String text = builder.toString();
            if (!(text.substring(0, 1).equals("{") && text.substring(text.length()-1).equals("}"))) {
                text = "\"" + text + "\"";
            }
            this.file = text;
        } else {
            this.file = file;
        }
    }

    public void setMerge_pdf(String merge_pdf) {
        this.merge_pdf = merge_pdf;
    }

    public String getName() {
        return name;
    }

    public String getTemplateguid() {
        return templateguid;
    }

    public String getFile() {
        return file;
    }

    public String getMerge_pdf() {
        return merge_pdf;
    }
}
