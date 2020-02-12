<?php
require_once 'vendor/autoload.php';

use ArturDoc\FilePdf;
use ArturDoc\Job;

$jobArturDoc = new Job();

if($_GET['jobguid']) {
    $jobGuid = $_GET['jobguid'];
} else {

    $job = $jobArturDoc->create(
        $_POST['TaskName'],
        $_POST['TemplateGuid'],
        $_POST['File'],
        $_POST['FileType'],
        $_POST['MergePdf'] ?? 0
    );
    $jobGuid = '';
    if (201 !== $job['code']) {
        echo '<span>'. $job['message'].'</span><br />';
        $jobGuid = '';
    } else {
        $jobGuid = $job['jobguid'];
    }
}

if ( '' !== $jobGuid) {
    $status = $jobArturDoc->getStatus($jobGuid);
    if ($status['status'] === $jobArturDoc::API_STATUS_READY) {
        $result = $jobArturDoc->getResult($jobGuid);
        FilePdf::getFileByResult($result, false);
    } else  {
        echo '<span>' . $status['status'] . '</span><br />';
        echo ' <a href="http://'.$_SERVER['HTTP_HOST']. '/arthurdoc.php?jobguid=' .$jobGuid.'">refresh</a>';
    }
}