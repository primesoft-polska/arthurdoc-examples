package pl.primesoft.arthurdoc;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.*;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.time.Duration;
import java.util.Scanner;
import java.util.concurrent.TimeoutException;
import java.util.logging.*;

public class Main {
    private static final HttpClient client = HttpClient.newBuilder().build();
    private static final  Scanner scan = new Scanner(System.in);
    private static final Logger logger = Logger.getLogger("ArthurDoc");
    private static String token;
    private static String URL;

    public static void main(String[] args) throws IOException, InterruptedException, TimeoutException {
        setupLogger();
        PropertiesLoader props = new PropertiesLoader();
        props.readProperties();
        token = props.getToken();
        URL = props.getUrl();
        RequestParams params = new RequestParams(args[0], args[1], args[2], args[3]);
        String guid = createNewJob(params);
        waitForFinish(guid);
        printJobResult(guid);
    }

    private static void printJobResult(String guid) throws IOException, InterruptedException, TimeoutException {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(URL + "/job_result/" + guid))
                .GET()
                .setHeader("X-AUTH-TOKEN", token)
                .header("charset", "utf-8")
                .timeout(Duration.ofMillis(60 * 1000L))
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() == 406) {
            logger.info("Job is still being processed");
            waitForFinish(guid);
            printJobResult(token);
        } else if (response.statusCode() != 200) {
            throw new RuntimeException("Unexpected error: " + response.body());
        } else {
            JSONObject jsonResponse = new JSONObject(response.body());
            JSONArray jsonArray = jsonResponse.getJSONArray("files");
            System.out.println("Files returned: " + jsonArray.length());
            for (Object fileInfo : jsonArray) {
                JSONObject file = ((JSONObject) fileInfo).getJSONObject("file");
                System.out.println();
                System.out.println("name: " + file.get("name"));
                System.out.println("url: " + file.get("url"));
            }
        }
    }

    private static String createNewJob(RequestParams params) throws IOException, InterruptedException {
        String json = createJson(params);
        HttpResponse<String> response = postNewJob(json);
        while (response.statusCode() == 401) {
            System.out.print("Authentication error. Enter token: ");
            token = scan.nextLine();
            response = postNewJob(json);
        }
        while (response.statusCode() == 400) {
            updateParams(params);
            json = createJson(params);
            response = postNewJob(json);
        }
        if (response.statusCode() >= 200 && response.statusCode() < 300) {
            String guid = extractGuid(response.body());
            System.out.println("Request accepted with guid: " + guid + ", waiting for the job to be finished...");
            return guid;
        } else {
            throw new RuntimeException("Unexpected response code: " + response);
        }
    }

    private static void waitForFinish(String guid) throws IOException, InterruptedException, TimeoutException {
        String status = checkJobStatus(guid);
        long start = System.currentTimeMillis();
        while (!status.equals("ready")) {
            Thread.sleep(2000);
            status = checkJobStatus(guid);
            if (start - System.currentTimeMillis() > 5 * 60 * 1000L) {
                throw new TimeoutException("5 minutes timeout exceeded, no more requests will be sent.");
            }
        }
    }

    private static String checkJobStatus(String guid) throws IOException, InterruptedException {
        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create(URL + "/job_status/" + guid))
                .GET()
                .setHeader("X-AUTH-TOKEN", token)
                .header("charset", "utf-8")
                .timeout(Duration.ofMillis(60 * 1000L))
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        if (response.statusCode() != 200) {
            throw new RuntimeException("Unexpected error: " + response.body());
        } else {
            return extractStatus(response.body());
        }
    }

    private static HttpResponse<String> postNewJob(String json) throws IOException, InterruptedException {
        HttpRequest request = HttpRequest.newBuilder()
                .POST(HttpRequest.BodyPublishers.ofString(json))
                .uri(URI.create(URL + "/job_new"))
                .setHeader("X-AUTH-TOKEN", token) // add request header
                .header("Content-Type", "application/json")
                .header("charset", "utf-8")
                .timeout(Duration.ofMillis(60 * 1000L))
                .build();
        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        logger.info(String.format("response: %s", response.body()));
        return response;
    }

    private static String extractGuid(String body) {
        JSONObject object = new JSONObject(body);
        return object.getString("jobguid");
    }

    private static String extractStatus(String body) {
        JSONObject object = new JSONObject(body);
        return object.getString("status");
    }

    private static String createJson(RequestParams params) {
        StringBuilder builder = new StringBuilder("{");
        builder.append("\"name\": \"")
                .append(params.getName())
                .append("\",\n\"templateguid\": \"")
                .append(params.getTemplateguid())
                .append("\",\n\"file\": ")
                .append(params.getFile())
                .append(",\n\"merge_pdf\": \"")
                .append(params.getMerge_pdf())
                .append("\"\n}");
        return builder.toString();
    }

    private static void updateParams(RequestParams params) throws IOException {
        System.out.println("Bad request, reenter parameters.");
        System.out.print("name: ");
        params.setName(scan.nextLine());
        System.out.print("templateguid: ");
        params.setTemplateguid(scan.nextLine());
        System.out.print("file (start with \"path:\" to read from file): ");
        params.setFile(scan.nextLine());
        System.out.print("merge_pdf: ");
        params.setMerge_pdf(scan.nextLine());
    }

    private static void setupLogger() throws IOException {
        // suppress the logging output to the console
        Logger rootLogger = Logger.getLogger("");
        Handler[] handlers = rootLogger.getHandlers();
        if (handlers[0] instanceof ConsoleHandler) {
            rootLogger.removeHandler(handlers[0]);
        }
        logger.setLevel(Level.INFO);
        FileHandler fileTxt = new FileHandler("Logger.txt");
        SimpleFormatter formatterTxt = new SimpleFormatter();
        fileTxt.setFormatter(formatterTxt);
        logger.addHandler(fileTxt);
    }

}
