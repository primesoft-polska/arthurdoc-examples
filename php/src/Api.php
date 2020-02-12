<?php

namespace ArturDoc;

use Exception;
use GuzzleHttp\Client;

class Api
{
    private $apiUrl = ApiConfig::API_URL;
    private $apiToken = ApiConfig::API_TOKEN;

    /**
     * @param string $method
     * @param string $params
     * @param array $dataParams
     * @param bool $isPost
     * @return array
     */
    protected function call(string $method, string $params = '', bool $isPost = false, array $dataParams = []): array
    {
        $client = new Client();
        try {
            $url = $this->apiUrl . $method;
            if ('' !== $params) {
                $url .= '/' . $params;
            }

            $response = $client->request(
                $isPost ? 'POST' : 'GET',
                $url,
                [
                    'body' => json_encode($dataParams),
                    'headers' => [
                        'X-AUTH-TOKEN' => $this->apiToken,
                        'accept' => 'application/json'
                    ]
                ]
            );

            $responseArr = json_decode((string)$response->getBody()->getContents(), true);
            $responseArr['code'] = $response->getStatusCode();

        } catch (Exception $exception) {
            $responseArr = [
                'code' => $exception->getCode(),
                'message' => $exception->getMessage()
            ];
        }

        return $responseArr;
    }
}