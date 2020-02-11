package pl.primesoft.arthurdoc;

import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class PropertiesLoader {
    private String token;
    private String url;

    PropertiesLoader() {
    }

    void readProperties() throws IOException {
        Properties props = new Properties();
        InputStream is = getClass().getClassLoader().getResourceAsStream("config.properties");
        if (is != null) {
            props.load(is);
        }
        token = props.getProperty("token");
        url = props.getProperty("apiUrl");
    }

    public String getToken() {
        return token;
    }
    public String getUrl() {
        return url;
    }
}
