<?php
namespace ArturDoc;

class FilePdf
{
    /**
     * @param string $url
     * @param string $name
     * @param bool $download
     */
    public static function getFile(string $url, string $name, $download = false):void
    {
        ob_clean();
        header('Content-type:application/pdf');
        $ContentDisposition = $download ? 'attachment' : 'inline';
        header('Content-Disposition: '.$ContentDisposition.'; filename="'.$name.'"');
        readfile($url);
        echo ob_get_clean();
    }

    /**
     * @param array $result
     * @param bool $download
     */
    public static function getFileByResult(array $result,bool $download): void
    {
        $files = $result['files'] ?? [];
        foreach ($files as $key => $file) {
            if (isset($file['file']['url'], $file['file']['name'])) {
                self::getFile($file['file']['url'], $file['file']['name'], $download);
            }
        }
    }

}