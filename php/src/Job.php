<?php
namespace ArturDoc;

class Job extends Api
{
    public const API_METHOD_JOB_NEW     = 'job_new';
    public const API_METHOD_JOB_STATUS  = 'job_status';
    public const API_METHOD_JOB_RESULT  = 'job_result';

    public const API_STATUS_ERROR   = 'error';
    public const API_STATUS_READY   = 'ready';
    public const API_STATUS_NEW     = 'new';

    public const API_TYPE_FILE     = 'json';
    public const API_TYPE_BASE4     = 'base64';



    /**
     * @param string $name
     * @param string $templateGuid
     * @param string $file
     * @param string $fileType
     * @param bool $mergePdf
     * @return array
     */
    public function create(string $name, string $templateGuid, string $file, string $fileType, bool $mergePdf): array
    {
        if(self::API_TYPE_FILE === $fileType){
            $file = json_decode($file, true);
        }

        $dataParams = [
            'name' => $name,
            'templateguid' => $templateGuid,
            'file' => $file,
            'merge_pdf' =>  (int)$mergePdf,
        ];

        return $this->call(self::API_METHOD_JOB_NEW, '', true, $dataParams);
    }

    /**
     * @param string $jobGuid
     * @return array
     */
    public function getStatus(string $jobGuid): array
    {
        return $this->call(self::API_METHOD_JOB_STATUS, $jobGuid);
    }

    /**
     * @param string $jobGuid
     * @return array
     */
    public function getResult(string $jobGuid): array
    {
        return $this->call(self::API_METHOD_JOB_RESULT, $jobGuid);
    }
}